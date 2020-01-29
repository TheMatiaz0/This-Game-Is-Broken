using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cyberevolver;
using Cyberevolver.Unity;
using UnityEngine;
using System.Collections;
using UnityEditor;
using UObj = UnityEngine.Object;
using System.Reflection;
using UnityEditorInternal;
using System.Collections.ObjectModel;

namespace Cyberevolver.EditorUnity
{
    public  class CyberAttributeException:CustomAttributeFormatException
    {
        public CyberAttributeException(Type attributeType,string message):base(message)
        {
            AttributeType = attributeType ?? throw new ArgumentNullException(nameof(attributeType));
        }

        public Type AttributeType { get; }
        

    }

    public sealed class CyberEdit 
    {
      
        private class GenericArrayComparer<T> : IEqualityComparer<T[]>
        {
            public bool Equals(T[] x, T[] y)
            {
                return StructuralComparisons.StructuralEqualityComparer.Equals(x, y);
            }

            public int GetHashCode(T[] obj)
            {
                return obj?.Sum(item => item.GetHashCode()) ?? 0;
            }
        }

        
        private CyberAttributeException error;
       
        private static bool lockSaving;

        public const BindingFlags SearchFlags = BindingFlags.Public | BindingFlags.Static | BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.DeclaredOnly;
        private readonly Dictionary<string, bool> ExpandedList = new Dictionary<string, bool>();
        private readonly Dictionary<object[], CyberEdit> nested = new Dictionary<object[], CyberEdit>(new GenericArrayComparer<object>());
        private readonly Dictionary<string, int> toolbarSelected = new Dictionary<string, int>();
        private readonly Dictionary<string, string[]> toolbarElement = new Dictionary<string, string[]>();
        private Dictionary<string, IGrouping<string, MemberInfo>> groups;
        private readonly static Dictionary<(FieldInfo, string), object> globalValues = new Dictionary<(FieldInfo, string), object>();
    
        public ReadOnlyDictionary<string, string[]> ToolbarElements => toolbarElement.AsReadOnly();
        public SerializedProperty CurrentProp { get; private set; }
        public FieldInfo CurrentField { get; private set; }
        public MemberInfo CurrentInspectedMember { get; private set; }
        public static CyberEdit Current { get; private set; }

       
        private readonly object[] deepWay;
        private SerializedProperty script;
        public ReadOnlyCollection<object> DeepWay => Array.AsReadOnly(deepWay);
        public SerializedObject SerializedObject { get; }
        public UObj Target { get; }
        public Cint HorizontalStack { get; private set; }
        public bool IsHorizontal => HorizontalStack > 0;
        public bool DontDrawScriptInfo { get; set; }
        public bool IsBreakDown => error == null;
        public void PushHorizontalStack()
        {
            HorizontalStack++;
        }
        public void PopHorizontalStack()
        {
            HorizontalStack--;
        }
      
        
       
        private IEnumerable<MemberInfo> members;

      
      
        public CyberEdit(SerializedObject serializedObject, UObj target,params object[] deepWay)
        {
            SerializedObject = serializedObject;
            Target = target;
            this.deepWay = deepWay ?? new string[0];
        }
        
        public void SetGlobalValue(FieldInfo field, string code, object value)
        {
            globalValues.AddOrSet((field, code), value);
        }
        public object GetGlobalValue(FieldInfo field, string code)
        {
            return globalValues.GetOrSetDefualt((field, code));
        }
      
        public CyberEdit GetNested(object[] val)
        {
            if(nested.TryGetValue(val,out CyberEdit result))
            {
                return result;
            }
            else
            {
                CyberEdit propCyber = new CyberEdit(CyberEdit.Current.SerializedObject, CyberEdit.Current.Target,val);
                propCyber.Active();
                propCyber.DontDrawScriptInfo = true;
                nested.Add(val, propCyber);
                return propCyber;
            }
        }

        public int GetToolbarSelect(string id)
        {
           if(toolbarSelected.TryGetValue(id,out int val))
            {
                return val;
            }
           else
            {
                toolbarSelected.Add(id, 0);
                return 0;
            }
          
        }
        public void SetToolbarSelect(string id, int value)
        {
            toolbarSelected.AddOrSet(id, value);
        }
        public void SetToolbarElementCollection(string id, string[] elements)
        {
            toolbarElement.AddOrSet(id, elements);
        }
      
        public Type GetFinalTargetType()
        {
           Type type= this.Target.GetType();
            foreach(object element in deepWay)
            {
                if(element is int)
                {
                    type = type.GetElementType();
                }
                else
                {
                    type = type.GetField(element.ToString(), BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).FieldType;
                }
                
            }
            return type;
        }
        public void Active()
        {
           
            Type targetType = GetFinalTargetType();
            script = this.SerializedObject.FindProperty("m_Script");
            
         
            members = GetTotalMembers(targetType);
            groups =
                (from item in members
                 let atr = item.GetCustomAttribute<GroupAttribute>()
                 where atr != null
                 group item by atr.Folder).ToDictionary(item => item.Key, item => item);

            if (TheEditor.HasInit)
                InvokeOnEnableDrawers();
        }
        public void InvokeOnEnableDrawers()
        {

            try
            {
                foreach (var member in members)
                    if (member is FieldInfo field)
                        TheEditor.DoOnAtr<IEnableInspectorDrawer>(field, (d, a) => d.DrawOnEnable(a, GetPropByName(field.Name), field));
                SerializedObject.ApplyModifiedProperties();
            }
            catch(CyberAttributeException exception)
            {
                error = exception;
                return;
            }
           
            
        }
      
      
        private IEnumerable<Type> GetAllTypeToSearch(Type t)
        {
            Type current = t;
           
            while (current != null)
            {
                yield return current;
                current = current.BaseType;
            }
        }
        private IEnumerable<MemberInfo> GetTotalMembers(Type type)
        {
           foreach(Type item in GetAllTypeToSearch(type))
            {
                foreach (var member in item.GetMembers(SearchFlags))
                {
                    if (member is MethodInfo method && method.GetCustomAttributes<CyberAttrribute>().Any())
                        yield return member;
                    else if (member is FieldInfo field && GetPropByName(field.Name) != null)
                        yield return member;    
                }
                  
                
            }        
        }
        public void SetExpand(string key,bool val)
        {
         
            ExpandedList[key] = val;
        }
        public bool GetExpand(string key)
        {
           
            return ExpandedList[key];
        }
      
      
        public void DrawPreScript()
        {
            if (script != null)
            {
                GUI.enabled = false;
                EditorGUILayout.PropertyField(script);
                GUI.enabled = true;
            }
        }

        public void Restore()
        {
            Current = this;
        }

        public void DrawClass(Type type)
        {

            TheEditor.DrawAlwaysBefore(type);
            TheEditor.DrawAlwaysAfter(type);
            DrawMainClass(type);
        }
       
        public void DrawAll()
        {


         
            SerializedObject.Update();
            Current = this;
            if (DontDrawScriptInfo == false)
                DrawPreScript();
            if (error != null)
            {
                CyberAttributeException error = (CyberAttributeException)this.error;
                EditorGUILayout.HelpBox($"Type:{error.AttributeType.Name}\n{error.Message}", UnityEditor.MessageType.Error);
                return;
            }
            DrawClass(Target.GetType());
            HashSet<string> alreadyDrawerFolders = new HashSet<string>(); 
          
            foreach (MemberInfo member in members)
            {
                if (member is MethodInfo)
                    continue;//fix by duck tape i know i will fix it before later
                try
                {
                    CurrentInspectedMember = member;
                    var group = member.GetCustomAttribute<GroupAttribute>();
                    if (group != null)
                    {
                        if (alreadyDrawerFolders.Contains(group.Folder))
                            continue;
                        alreadyDrawerFolders.Add(group.Folder);
                        if (ExpandedList.ContainsKey(group.Folder) == false)
                        {
                            ExpandedList.Add(group.Folder, false);
                        }
                       
                        DrawGroup(groups[group.Folder]);
                    }
                    else
                        DrawMember(member);
                }
                catch (CyberAttributeException except)
                {
                    error = except;
                    return;
                }
                finally
                {
                    CurrentProp = null;
                    CurrentInspectedMember = null;
                    CurrentField = null;
                }
              
                
                
            }
            //fix by duck tape i know i will fix it before later
            foreach (var item in members)
                if (item is MethodInfo method)
                    TheEditor.DrawMethod(method);
            TheEditor.ClearEnderDrawers();
              
         
        }
        public void DrawMember(MemberInfo member)
        {
           
           
            if (member is FieldInfo field)
            {


                CurrentProp = GetPropByName(field.Name);
                CurrentField = field;
                if (TheEditor.DrawProperty(field, CurrentProp) == false)
                {
                    lockSaving = true;
                }
                CurrentProp = null;
                CurrentField = null;
            }
            else if (member is MethodInfo method)
            {
                TheEditor.DrawMethod(method);
            }
        }
        public void Save()
        {
            if (lockSaving == false)
                SerializedObject.ApplyModifiedProperties();
            lockSaving = false;

        }
        public SerializedProperty GetPropByName(string name)
        {
            if(deepWay.Length==0)
            {
                return SerializedObject.FindProperty(name);
            }
            else
            {
                SerializedProperty prop = SerializedObject.FindProperty(deepWay[0].ToString());
                for (int x = 1; x < deepWay.Length; x++)
                {

                    object element = deepWay[x];
                    if(element is int i)
                    {
                        prop = prop.GetArrayElementAtIndex(i);
                    }
                    else
                    {
                        prop = prop.FindPropertyRelative(deepWay[x].ToString());
                    }
                        
                 
                }
                return prop.FindPropertyRelative(name);
            }
        }
        public FieldInfo GetFieldByName(string name)
        {
           Type t= Target.GetType();
          foreach(object item in deepWay)
            {
                if (item is int)
                    break; 
                t = t.GetField(item.ToString()).FieldType;
            }
            return t.GetField(name);
        }

        public void DrawGroup(IGrouping<string, MemberInfo> group)
        {

            if(TheEditor.CanDrawGroup(CyberEdit.Current.GetFinalTargetType()))
            {
                TheEditor.DrawOnAttribute<IGroupDrawer>(group.First(),
                (d, a)
                =>
                {
                    d.DrawGroup(group);
                    return true;
                });
            }
           


        }
       

        public void DrawMainClass(Type type)
        {
            TheEditor.DoOnAtr<IClassDrawer>(type, (d, a) => d.Draw(a));
        }
    }
}
