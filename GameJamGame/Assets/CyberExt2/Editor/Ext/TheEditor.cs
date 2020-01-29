using Cyberevolver.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace Cyberevolver.EditorUnity
{
    public  static class TheEditor 
    {
        private readonly static Dictionary<object, GuiRemeber> members = new Dictionary<object, GuiRemeber>();
        private static Dictionary<Type, ICyberDrawer> drawers;
      
        private readonly static Stack<(CyberAttrribute atr, IEnderDrawer drawer)> enderDrawers = new Stack<(CyberAttrribute, IEnderDrawer)>();
        public static bool HasInit { get; private set; } = false;
#pragma warning disable IDE0051
        [UnityEditor.Callbacks.DidReloadScripts]
        private static void OnCompile()
        {
            drawers =
                (from ass in AppDomain.CurrentDomain.GetAssemblies()
                 from type in ass.GetTypes()
                 where type.GetInterfaces().Contains(typeof(ICyberDrawer))
                 let atr = type.GetCustomAttribute<DrawerAttribute>()
                 where atr != null
                 select new KeyValuePair<Type, ICyberDrawer>(atr.Target, (ICyberDrawer)Activator.CreateInstance(type)))
                .ToDictionary(i => i.Key, v => v.Value);
            HasInit = true;
            if (CyberEdit.Current != null)
                CyberEdit.Current.InvokeOnEnableDrawers();
        }
    
#pragma warning restore IDE0051
    private class GuiRemeber
        {
            private GuiRemeber() { }
            public Color Color { get; set; }
            public bool Enable { get; set; }
            public Color BackgroundColor { get;set; }
            public Color ContentColor { get;set; }
            public string ToolTip { get; set; }
            public GUISkin Skin { get; set; }
            public static GuiRemeber Create()
            {
                var remebered = new GuiRemeber
                {
                    Color = GUI.color,
                    Enable = GUI.enabled,
                    BackgroundColor = GUI.backgroundColor,
                    ContentColor = GUI.contentColor,
                    ToolTip = GUI.tooltip,
                    Skin = GUI.skin
                };
                return remebered;

            }
            public void Restore()
            {
                GUI.color = this.Color;
                GUI.enabled = Enable;
                GUI.backgroundColor = BackgroundColor;
                GUI.contentColor = ContentColor;
                GUI.tooltip = ToolTip;
                GUI.skin = Skin;
            }
            
        }
        public static void Pop()
        {
            var (atr, drawer) = enderDrawers.Pop();
            drawer.DrawEnd(atr);
        }
        public static void ClearEnderDrawers()
        {
            foreach (var (atr, drawer) in enderDrawers)
                drawer.DrawEnd(atr);
            enderDrawers.Clear();
        }
        public static void DrawMethod(MethodInfo method)
        {
            DrawAlwaysBefore(method);
            if (TheEditor.CanDrawMemeber(method))
            {
                
           
               
                TheEditor.DrawMetaBefore(method);
                TheEditor.DrawOnlyFirst<IMethodDrawer>(method, (d, a) => d.DrawMethod(method,a));
                TheEditor.DrawMetaAfter(method);       
                TheEditor.PutEnderDrawers(method);
                
                
            }
            DrawAlwaysAfter(method);

        }
        public static void PutEnderDrawers(MemberInfo field)
        {
            TheEditor.DoOnAtr<IEnderDrawer>(field,
                (d, a) =>
                {
                    var m = (a, d);
                    enderDrawers.Push(m);
                 

                });
        }

        public static bool DrawProperty(FieldInfo field, SerializedProperty prop, bool ignoreDirectly = false,bool dontDrawContent=false)
        {
            int lastIndent = 0;
            try
            {
               
                bool before = GUI.enabled;

                bool res = true;

                DrawAlwaysBefore(field);
                if (TheEditor.CanDrawMemeber(field))
                {
                    lastIndent = EditorGUI.indentLevel;
                   
                    var (style, customName, customStyle) = TheEditor.GetStyle(field);
                    TheEditor.DrawMetaBefore(field);
                    if (CyberEdit.Current?.IsHorizontal ?? false)
                    {
                        EditorGUI.indentLevel = 0;
                    }
                    GUIContent content;

                    if (customName == null)
                    {
                        content = new GUIContent(prop.displayName.Replace("k__Backing Field", "")
                    .Replace("<", "")
                    .Replace(">", ""));
                    }
                    else
                        content = new GUIContent(customName);

                    if (dontDrawContent)
                        content = GUIContent.none;

                    var directly = TheEditor.GetDirectlyDrawer(field);


                    if (ignoreDirectly || directly == default)
                    {
                        if (prop.isArray == false || prop.propertyType == SerializedPropertyType.String)
                        {
                            var showCyberAtt = field.FieldType.GetCustomAttribute<ShowCyberInspectorAttribute>();
                            if (showCyberAtt == null)
                            {
                                if (customStyle || field.GetCustomAttributes<CyberAttrribute>().Any(atr => drawers[atr.GetType()] is IPrefixDrawer))
                                {
                                    EditorGUILayout.BeginHorizontal();

                                    if (customStyle)
                                    {
                                        TheEditor.DrawPrefix(content, field, style);
                                    }
                                    else
                                    {
                                        TheEditor.DrawPrefix(content, field);
                                    }
                                    EditorGUILayout.PropertyField(prop,
                                       GUIContent.none, GUILayout.MinWidth(10));
                                    EditorGUILayout.EndHorizontal();
                                }
                                else
                                {
                                    EditorGUILayout.PropertyField(prop, content);
                                }

                            }
                            else
                            {
                                DrawNestesInspector(showCyberAtt, field, prop, content, style, CyberEdit.Current.DeepWay.Append(field.Name).ToArray());


                            }

                        }
                        else
                            TheEditor.DrawArray(field, content, prop);
                    }
                    else
                    {
                        directly.drawer.DrawDirectly(prop, directly.atr, content, style, field);
                    }

                    object obj;
                    if ((obj = prop.GetJustValue()) != null)
                    {
                        if (TheEditor.IsGoodValue(field, obj) == false)
                            res = false;


                    }
                    PutEnderDrawers(field);
                    TheEditor.DrawMetaAfter(field);

                    GUI.enabled = before;
                }
                DrawAlwaysAfter(field);
                return res;

            }
            catch
            {
                throw;
            }
            finally
            {
                if (CyberEdit.Current?.IsHorizontal ?? false)
                    EditorGUI.indentLevel += lastIndent;
            }
           


          

        }
        private static CyberAttrribute DrawModifer<IDrawer>(Action<IGroupModifer, CyberAttrribute> action, string folder)
           where IDrawer : class, IGroupModifer
        {
            CyberAttrribute atr = null;
            TheEditor.DrawOnlyFirst<IDrawer>(CyberEdit.Current.GetFinalTargetType(), (d, a) =>
            {

                if ((a as GroupModiferAttribute).Folder == folder)
                {
                    action(d, a);
                    atr = a;
                }
                   
               
            });
            return atr;
        }
        public static CyberAttrribute DrawBeforeGroup<IDrawer>(string folder)
              where IDrawer : class, IGroupModifer
        {
           return DrawModifer<IDrawer>((d, a) => d.BeforeGroup(a), folder);
        }
        public static CyberAttrribute DrawAfteGroup<IDrawer>(string folder)
              where IDrawer : class, IGroupModifer
        {
         return   DrawModifer<IDrawer>((d, a) => d.AfterGroup(a), folder);
        }
        public static void DrawNestesInspector(ShowCyberInspectorAttribute inspectorAttribute,FieldInfo field,SerializedProperty prop,GUIContent content,GUIStyle style,object[] nested, bool indentLv = true)
        {


            void ChangeIndent(int val)
            {
                if (indentLv)
                    EditorGUI.indentLevel += val;
            }
            bool shouldBeEnded = false;
            void BeginVectical()
            {
                TheEditor.BeginVertical(inspectorAttribute.BackgroundMode);
                shouldBeEnded = true;

            }
            void DrawMainNested()
            {
                if (inspectorAttribute.NameIn == false)
                    BeginVectical();
                   var beforeEditor = CyberEdit.Current;
                CyberEdit.Current.Save();
              
                var propCyber = CyberEdit.Current.GetNested(nested);
                propCyber.DrawAll();
              
                beforeEditor.Restore();
              
            }
            if (inspectorAttribute.NameIn)
                BeginVectical();
            switch (inspectorAttribute.CyberObjectMode)
            {
                case CyberObjectMode.Expandable:
                 
                    if (prop.isExpanded = EditorGUILayout.Foldout(prop.isExpanded, content, true))
                    {
                        ChangeIndent(1);
                        DrawMainNested();
                        ChangeIndent(-1);
                    }
                        
                  
                    break;
                case CyberObjectMode.InOneLine:
                    EditorGUILayout.BeginHorizontal();
                    TheEditor.DrawPrefix(content, field, style);
                    DrawMainNested();
                    EditorGUILayout.EndHorizontal();
                    break;
                case CyberObjectMode.AlwaysExpanded:    
                    TheEditor.DrawPrefix(content, field, style);
                    ChangeIndent(1);
                    DrawMainNested();
                    ChangeIndent(-1);
                    break;



            }
            if (shouldBeEnded)
                EditorGUILayout.EndVertical();
        }
       
        public static void DrawArray(FieldInfo member, GUIContent content, SerializedProperty prop
            ,bool drawExpand=true,bool drawSize=true,bool indentLv=true , string[] customNames=null)
        {
            if (drawExpand == false|| (prop.isExpanded = EditorGUILayout.Foldout(prop.isExpanded, content, true)))
            {
                if (indentLv)
                    EditorGUI.indentLevel++;
                if (drawSize)
                    DrawSize(member, prop);
                int index = 0;
                foreach (var elementProp in prop.ToEnumerable())
                {
                    GUIContent customConent = null;
                    if (customNames != null && customNames.Length > index)
                        customConent = new GUIContent(customNames[index]);
                    
                    
                        
                   if (TryDrawNestedInspectorForArray(member.FieldType.GetElementType(), index, prop.name, elementProp,true, customConent) == false)
                    {
                        if (customConent!=null)
                            EditorGUILayout.PropertyField(elementProp,customConent);
                        else
                            EditorGUILayout.PropertyField(elementProp);
                    }
                      
                    index++;
                }
                if (indentLv)
                    EditorGUI.indentLevel--;
                DrawAfterArray(member, prop);
            }


        }
        private static bool TryDrawNestedInspectorForArray(Type type,int index,string arrayName,SerializedProperty prop,bool indentLv=true , GUIContent customContent=null)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            var attribute = type.GetCustomAttribute<ShowCyberInspectorAttribute>();
            if (attribute == null)
                return false;

            DrawNestesInspector(attribute, null, prop,customContent?? new GUIContent($"{index}:"), new GUIStyle(), CyberEdit.Current.DeepWay.Append(arrayName).Append(index).ToArray(), indentLv);

            return true;
        }

        public static (IDirectlyDrawer drawer, CyberAttrribute atr) GetDirectlyDrawer(FieldInfo field)
        {
            (IDirectlyDrawer drawer, CyberAttrribute atr) result = default;
            DrawOnAttribute<IDirectlyDrawer>(field,
                (d, a) =>
                {
                    result = (d, a);
                    return true;
                }
                );
            return result;
        }
        public static void DrawAlwaysBefore(MemberInfo field)
        {
            TheEditor.DoOnAtr<IAlwaysDrawer>(field, (d, a) => d.DrawBefore(a));
        }
        public static void DrawAlwaysAfter(MemberInfo field)
        {
            TheEditor.DoOnAtr<IAlwaysDrawer>(field, (d, a) => d.DrawAfter(a));
        }
        public static void DrawPrefix(string content, FieldInfo field, GUIStyle style = null)
        {
            DrawPrefix(new GUIContent(content), field, style);
        }
        public static bool DrawBeforeArraySize(FieldInfo member, SerializedProperty prop)
        {
            EditorGUILayout.BeginHorizontal();
            var res= DoOnAtr<IArrayModiferDrawer>(member, (d, a) => d.DrawBeforeSize(prop, a));
            EditorGUILayout.EndHorizontal();
            return res;
        }
        public static bool DrawAfterArraySize(FieldInfo member, SerializedProperty prop)
        {
            return DoOnAtr<IArrayModiferDrawer>(member, (d, a) => d.DrawAfterSize(prop, a));
        }
        public static bool DrawAfterArray(FieldInfo member, SerializedProperty prop)
        {
            return DoOnAtr<IArrayModiferDrawer>(member, (d, a) => d.DrawAfterAll(prop, a));
        }

        public static bool DrawOnlyFirst<TDrawer>(MemberInfo member, Action<TDrawer, CyberAttrribute> action)
              where TDrawer : class, ICyberDrawer
        {
            bool res = false;
            DrawOnAttribute<TDrawer>(member,
                (d, a)
                =>
                {
                    action(d, a);
                    res = true;
                    return true;
                });
            return res;
        }
        public static (GUIStyle style, string customName, bool minOne) GetStyle(MemberInfo field)
        {
            var style = new GUIStyle();
            string customName = null;
            bool res = false;
            DoOnAtr<IStyleDrawer>(field, (d, a)
                =>
            {
                d.ChangeGuiStyle(a, ref style, ref customName);
                res = true;

            });
            return (style, customName, res);
        }
        public static bool IsGoodValue(FieldInfo field, object value)
        {
            bool anyBad = false;
            DrawOnAttribute<ILimitDrawer>(field,
                (d, a) =>
                {
                    if (d.IsGoodValue(a, value) == false)
                    {
                        anyBad = true;
                        return true;
                    }
                    return false;
                });



            return !anyBad;
        }
        public static void DrawMetaBefore(MemberInfo field)
        {
            DoOnAtr<IMetaDrawer>(field, (d, a) => d.DrawBefore(a));
        }
        public static void DrawMetaAfter(MemberInfo field)
        {
            DoOnAtr<IMetaDrawer>(field, (d, a) => d.DrawAfter(a));
        }


        public static bool CanDrawMemeber(MemberInfo member)
        {
          

            return CanDraw<IConditionsDrawer>(member, (d, a) => d.CanDraw(a));
           
        }
        public static bool CanDrawGroup(Type type)
        {
            return CanDraw<IGroupConditionsDrawer>(type, (d, a) => d.CanDraw(a));
        }
        public static bool CanDraw<TDrawer>(MemberInfo member,Func<TDrawer,CyberAttrribute,bool> func)
            where TDrawer:class, ICyberDrawer
        {
            bool res = true;
            DrawOnAttribute<TDrawer>(member,
                (d, atr)
                =>
                {
                    if (func(d, atr) == false)
                    {
                        res = false;
                        return true;
                    }                                  
                    return false;
                });
            return res;
        }
        public static void DrawOnAttribute<TDrawer>(MemberInfo member, Func<TDrawer, CyberAttrribute, bool> action)
            where TDrawer : class, ICyberDrawer
        {
            if (member is null)
            {
                throw new ArgumentNullException(nameof(member));
            }

            foreach (var atr in member.GetCustomAttributes<CyberAttrribute>())
            {
               
                if (drawers.ContainsKey(atr.GetType()) == false)
                    continue;
                var drawer = (drawers[atr.GetType()] as TDrawer);
               
                if (drawer != null)
                {
                    if (member is FieldInfo field && CyberEdit.Current != null)
                    {

                        CyberAttributeUsageAttribute drawerConditionsAtt = atr.GetType().GetCustomAttribute<CyberAttributeUsageAttribute>();
                        if (drawerConditionsAtt != null&&(CyberEdit.Current?.CurrentProp!=null))
                        {
                            if (drawerConditionsAtt.Flag.IsGoodWith(CyberEdit.Current.CurrentProp) == false)
                                throw new CyberAttributeException(drawer.GetType(), $"This attribute requirers {drawerConditionsAtt.Flag}");
                        }

                    }
                    if (action(drawer, atr))
                        return;
                }
                   
            }
        }
        public static bool DoOnAtr<TDrawer>(MemberInfo field, Action<TDrawer, CyberAttrribute> action)
            where TDrawer : class, ICyberDrawer
        {
            bool res = false;
            DrawOnAttribute<TDrawer>(field,
                (d, a) =>
                {
                    action(d, a);
                    res = true;
                    return false;
                }
              );
            return res;
        }
        public static void DrawSize(FieldInfo field, SerializedProperty prop)
        {
            TheEditor.DrawBeforeArraySize(field, prop);
            EditorGUILayout.BeginHorizontal();
            DrawPrefix("Size", field);
            prop.arraySize = EditorGUILayout.IntField(prop.arraySize);
            EditorGUILayout.EndHorizontal();
            TheEditor.DrawAfterArraySize(field, prop);

        }
        
        public static bool DrawPrefix(GUIContent content, FieldInfo field, GUIStyle style = null)
        {


            if (field==null||DrawOnlyFirst<IPrefixDrawer>(field, (d, a) => d.DrawPrefix(content, style, a)) == false)
            {
                if (field == null || field.GetCustomAttribute<HidePrefixAttribute>() == null)
                {
                    TheEditor.RawPrefixLable(content);
                    return true;
                }
                else return false;

            }
            return true;
        }
        public static bool AnyCustomPrefix(FieldInfo field)
        {
            bool any = false;
            DrawOnlyFirst<IPrefixDrawer>(field, (d, a) => any = true);
            return field.GetCustomAttribute<HidePrefixAttribute>() != null || any == true;
        }
        /// <param name="content">Can be null</param>
        /// <param name="style">Can be null</param>

        public static void DrawPropertyAsDropdown<TElement>(SerializedProperty prop, FieldInfo field, GUIContent content, GUIStyle style, TElement[] elements, Action<TElement> onClick
            , Func<TElement, int, string> nameGenerator = null, bool drawPrefix = true,bool showAsName=true)
        {
            EditorGUILayout.BeginHorizontal();
            if (drawPrefix)
                TheEditor.DrawPrefix(content, field, style);

            if (EditorGUILayout.DropdownButton(new GUIContent(((showAsName==false)?(prop.GetJustValue()?.ToString()??"null"):nameGenerator((TElement)prop.GetJustValue(),(int)elements.GetIndex(item=>item.Equals(prop.GetJustValue()))))), FocusType.Passive))
            {
                    
                GenericMenu genericMenu = new GenericMenu();
                int index = 0;
                foreach (TElement item in elements)
                {
                    genericMenu.AddItem(new GUIContent(nameGenerator?.Invoke(item, index) ?? item?.ToString() ?? "null"), false, (object element) =>
                    {

                        onClick((TElement)element);
                        prop.serializedObject.ApplyModifiedProperties();

                    }, item);
                    index++;
                }


                genericMenu.ShowAsContext();

            }
            EditorGUILayout.EndHorizontal();
        }
        public static void DrawPropertyAsDropdownWithFixValue(SerializedProperty prop, FieldInfo field, GUIContent content, GUIStyle style, object[] elements,
            Action<object> put, object value,
            Func<object, int, string> nameGenerator = null, bool drawPrefix = true,bool showAsName=true)
        {

            if (elements.Any(item => item.Equals(value)) == false)
            {
                if (elements.Length > 0)
                    put(elements[0]);
                else
                    put(null);
            }
            DrawPropertyAsDropdown<object>(prop, field, content, style, elements, (element) => put(element), nameGenerator, drawPrefix,showAsName);
        }
        public static void DrawPropertyAsDropdown<TElement>(SerializedProperty prop, TElement[] elements, Action<TElement> onClick
            , Func<TElement, int, string> nameGenerator = null)
        {
            DrawPropertyAsDropdown(prop, null, null, null, elements, onClick, nameGenerator, false);
        }

        public static void RawPrefixLable(GUIContent content=null, GUIStyle style=null)
        {
#pragma warning disable IDE0054 
            content = content??new GUIContent();

            if (style != null)
                EditorGUILayout.PrefixLabel(content, new GUIStyle(), style);
            else
                EditorGUILayout.PrefixLabel(content);
        }
        public static void DrawBasicGroup(IGrouping<string,MemberInfo> group,BackgroundMode mode)
        {
            TheEditor.BeginVertical(mode);
            EditorGUILayout.LabelField(group.Key, new GUIStyle() { fontStyle = FontStyle.Bold });
            foreach (FieldInfo item in group)
            {
                TheEditor.DrawProperty(item,CyberEdit.Current.GetPropByName(item.Name));
            }
            EditorGUILayout.EndVertical();
        }
        public static void HelpBox(string content, MessageType type, GUIStyle style)
        {
            JumpBeetwenStyles(style,
                () =>
                {
                    EditorGUILayout.HelpBox(content,(UnityEditor.MessageType)type);
                }, () => EditorStyles.helpBox);
        }
        public static void PrepareToRefuseGui(object refuser)
        {
            var remembed = GuiRemeber.Create();
            members.Add(refuser, remembed);
        }
        public static void RefuseGui(object refuser)
        {
            members[refuser].Restore();
            members.Remove(refuser);
        }
        public static bool TryRefuseGui(object refuser)
        {
            bool res;
            if (res = members.TryGetValue(refuser, out GuiRemeber result))
            {
                result.Restore();
                members.Remove(refuser);
            }
            return res;
        }
        public static void ShortLabelField(GUIContent content,GUIStyle style,params GUILayoutOption[] options)
        {
            Vector2 size = style.CalcSize(content);
            options = (options ?? Enumerable.Empty<GUILayoutOption>()).Append(GUILayout.Width(size.x)).ToArray();
            EditorGUILayout.LabelField(content, style, options);
        }


        private static void JumpBeetwenStyles(GUIStyle style, Action beetwenAction,Func<GUIStyle> getOriginal)
        {
            var template = new GUIStyle();
            bool CanSet(PropertyInfo prop)
            {
                return prop.SetMethod != null &&prop.PropertyType.IsClass==false&&!System.Object.Equals(prop.GetValue(style),(prop.GetValue(template)));
            }
            if (beetwenAction == null)
                throw new ArgumentNullException(nameof(beetwenAction));
            var original = getOriginal();
            GUIStyle before = new GUIStyle();//I have to do it. That's how unity works. It even doesn't impelement clone method :(
            var properties = typeof(GUIStyle).GetProperties(BindingFlags.Public|BindingFlags.Instance);

            foreach (PropertyInfo prop in properties)
            {
                if(CanSet(prop))
                {
                    prop.SetValue(before, prop.GetValue(original));
                    prop.SetValue(original, prop.GetValue(style));
                }
            }      
            beetwenAction();
            foreach (PropertyInfo prop in properties)
                if (CanSet(prop))
                    prop.SetValue(original, prop.GetValue(before));
           

        }

        public static Rect BeginHorizontal(BackgroundMode backgroundMode)
        {
            if (backgroundMode == BackgroundMode.None)
                return EditorGUILayout.BeginHorizontal();
            else
               return EditorGUILayout.BeginHorizontal(backgroundMode.ToString());
        }
        public static Rect BeginVertical(BackgroundMode backgroundMode)
        {
            if (backgroundMode == BackgroundMode.None)
                return EditorGUILayout.BeginVertical();
            else
                return EditorGUILayout.BeginVertical(backgroundMode.ToString());
        }
        
        public static bool CheckEquals(SerializedProperty prop, object val, Equaler equaler)
        {
            object current = prop.GetJustValue();
            try
            {
                return equaler.CheckEquals(current, Convert.ChangeType(val, current.GetType()));
            }
            catch(Exception e) { throw new CustomAttributeFormatException($"Error during comparing {val.GetType().Name} and {current.GetType().Name}",e); }
        }

        public static (string, UnityEngine.Object) GeneralField(string value, UnityEngine.Object reference, Type type, params GUILayoutOption[] options)
        {
            if (type == typeof(string))
                return (EditorGUILayout.TextField(value, options), null);
            else if (type.IsSubclassOf(typeof(Enum)))
            {
                if (Enum.GetNames(type).Any(item => item == value) == false)
                    value = Enum.GetNames(type)[0];
                return (EditorGUILayout.EnumPopup((Enum)Enum.Parse(type, value), options).ToString(), null);
            }
            else if (type.IsNumber())
            {
                switch (Type.GetTypeCode(type))
                {

                    case TypeCode.Int16:
                    case TypeCode.Int32:
                    case TypeCode.Char:
                    case TypeCode.UInt16:
                    case TypeCode.UInt32:
                        return BySimpleParser<int>(int.Parse, EditorGUILayout.IntField);
                    case TypeCode.Int64:
                    case TypeCode.UInt64:
                        return BySimpleParser<long>(long.Parse, EditorGUILayout.LongField);
                    case TypeCode.Single:
                        return BySimpleParser<float>(float.Parse, EditorGUILayout.FloatField);
                    case TypeCode.Double:
                        return BySimpleParser<double>(double.Parse, EditorGUILayout.DoubleField);
                    default: throw new ArgumentException("Not supported number type", nameof(type));
                }

            }
            else if (type == typeof(Color))
            {
                Color color = default;
                try
                {
                    color = ColorExtension.Parse(value);
                }
                catch { }

                return (EditorGUILayout.ColorField(color, options).ToString(), null);
            }
            else if (type == typeof(Vector2))
                return ByAdvParser(Vector2Extension.Parse, EditorGUILayout.Vector2Field);
            else if (type == typeof(Vector2Int))
                return ByAdvParser<Vector2Int>((string text) => new Vector2Int((int)Vector2Extension.Parse(text).x, (int)Vector2Extension.Parse(text).y), EditorGUILayout.Vector2IntField);

            else if (type.IsSubclassOf(typeof(UnityEngine.Object)) || type == typeof(UnityEngine.Object))
            {

#pragma warning disable CS0618
                return (null, EditorGUILayout.ObjectField(reference, type, options));
#pragma warning restore CS0618
            }
            else
                throw new ArgumentException("Not supported type", nameof(type));

            (string, UnityEngine.Object) BySimpleParser<T>(Func<string, T> parser, Func<T, GUILayoutOption[], T> fielder)
                where T : struct
            {
                T result;
                try
                {
                    result = parser(value);
                }
                catch
                {
                    result = default;
                }

                return (fielder(result, options).ToString(), null);
            }
            (string, UnityEngine.Object) ByAdvParser<T>(Func<string, T> parser, Func<string, T, GUILayoutOption[], T> fielder)
            where T : struct
            {
                return BySimpleParser<T>(parser, (T a, GUILayoutOption[] option) => fielder("", a, option));
            }
        }
        public static (string, UnityEngine.Object) GeneralField(string value, Rect pos, UnityEngine.Object reference, Type type)
        {
            if (type == typeof(string))
                return (EditorGUI.TextField(pos, value), null);
            else if (type.IsSubclassOf(typeof(Enum)))
            {
                if (Enum.GetNames(type).Any(item => item == value) == false)
                    value = Enum.GetNames(type)[0];
                return (EditorGUI.EnumPopup(pos, (Enum)Enum.Parse(type, value)).ToString(), null);
            }
            else if (type.IsNumber())
            {
                switch (Type.GetTypeCode(type))
                {

                    case TypeCode.Int16:
                    case TypeCode.Int32:
                    case TypeCode.Char:
                    case TypeCode.UInt16:
                    case TypeCode.UInt32:
                        return BySimpleParser<int>(int.Parse, EditorGUI.IntField);
                    case TypeCode.Int64:
                    case TypeCode.UInt64:
                        return BySimpleParser<long>(long.Parse, EditorGUI.LongField);
                    case TypeCode.Single:
                        return BySimpleParser<float>(float.Parse, EditorGUI.FloatField);
                    case TypeCode.Double:
                        return BySimpleParser<double>(double.Parse, EditorGUI.DoubleField);
                    default: throw new ArgumentException("Not supported number type", nameof(type));
                }

            }
            else if (type == typeof(Color))
                return BySimpleParser(ColorExtension.Parse, EditorGUI.ColorField);
            else if (type == typeof(Vector2))
                return ByAdvParser(Vector2Extension.Parse, EditorGUI.Vector2Field);
            else if (type == typeof(Vector2Int))
                return ByAdvParser<Vector2Int>((string text) => new Vector2Int((int)Vector2Extension.Parse(text).x, (int)Vector2Extension.Parse(text).y), EditorGUI.Vector2IntField);
            else if (type.IsSubclassOf(typeof(UnityEngine.Object)) || type == typeof(UnityEngine.Object))
            {
#pragma warning disable CS0618
                return (null, EditorGUI.ObjectField(pos, reference, type));
#pragma warning restore CS0618
            }
            else
                throw new ArgumentException("Not supported type", nameof(type));

            (string, UnityEngine.Object) BySimpleParser<T>(Func<string, T> parser, Func<Rect, T, T> fielder)
                where T : struct
            {
                T result;
                try
                {
                    result = parser(value);
                }
                catch (FormatException)
                {
                    result = default;
                }

                return (fielder(pos, result).ToString(), null);
            }
            (string, UnityEngine.Object) ByAdvParser<T>(Func<string, T> parser, Func<Rect, string, T, T> fielder)
               where T : struct
            {
                return BySimpleParser<T>(parser, (Rect r, T a) => fielder(r, "", a));
            }
        }

    }
}
