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
using UnityEditorInternal;

namespace Cyberevolver.EditorUnity
{
    [CustomPropertyDrawer(typeof(BaseSerializeDictionary),true)]
    public class DictioniaryDrawer : PropertyDrawer
    {
        private ReorderableList reorderable;
        
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return 0;
        }

      
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {

            DictioniarySettingsAttribute attribute = CyberEdit.Current.CurrentField?.GetCustomAttributes(true).OfType<DictioniarySettingsAttribute>().FirstOrDefault();
            attribute = attribute ?? new DictioniarySettingsAttribute(false, true);
            
            SerializedProperty keys = property.FindPropertyRelative("keys");
            SerializedProperty values = property.FindPropertyRelative("values");
            bool advValue = ((attribute?.TryDoReordable ) == false) || (values.arraySize > 0 && values.GetArrayElementAtIndex(0).propertyType == SerializedPropertyType.Generic);
            if (advValue==false&&reorderable==null)
            {
                reorderable = new ReorderableList(CyberEdit.Current.SerializedObject, keys, true, true, true, true)
                {
                    drawHeaderCallback =
                    (Rect rect) =>
                    {
                        EditorGUI.LabelField(rect, property.displayName);
                    },

                    drawElementCallback =   
                   (Rect rect, int i, bool isActive, bool isFocused) =>
                   {
                       rect.width /= 2;
                       EditorGUI.PropertyField(rect, keys.GetArrayElementAtIndex(i), GUIContent.none);
                       rect.x += rect.width;
                       EditorGUI.PropertyField(rect, values.GetArrayElementAtIndex(i), GUIContent.none);
                   }
                };

            }
              
            ChangeSize(keys.arraySize);
            void ChangeSize(int newSize)
            {
                keys.arraySize = newSize;
                values.arraySize = newSize;
            }
            void RemoveElementAt(int ind)
            {
                keys.DeleteArrayElementAtIndex(ind);
                values.DeleteArrayElementAtIndex(ind);
            }

            IEnumerable<(SerializedProperty key, SerializedProperty value)> connected = keys.ToEnumerable().Zip(values.ToEnumerable(), (key, value) => (key, value));


            bool IsExpand() => ((attribute?.Exandable) == false || (property.isExpanded = EditorGUILayout.Foldout(property.isExpanded, label, true)));



            int index = 0;
            if (advValue && IsExpand())
            {
                EditorGUI.indentLevel++;

                foreach ((SerializedProperty key, SerializedProperty value) in connected)
                {
                    index++;
                    EditorGUILayout.BeginVertical("GroupBox");

                    EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.BeginHorizontal("HelpBox");
                    EditorGUILayout.PropertyField(key, new GUIContent($"Key:"));

                    TheEditor.PrepareToRefuseGui(this);
                    GUI.color = Color.red;


                    EditorGUILayout.EndHorizontal();


                    TheEditor.RefuseGui(this);
                    EditorGUILayout.EndHorizontal();
                    EditorGUI.indentLevel++;
                    EditorGUILayout.PropertyField(value, new GUIContent($"Value:"));
                    EditorGUI.indentLevel--;
                    EditorGUILayout.EndVertical();

                }


                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("");


                TheEditor.PrepareToRefuseGui(this);
                GUI.color = Color.green;
                if (GUILayout.Button("+", GUILayout.MinWidth(10), GUILayout.MaxWidth(30)))
                {
                    ChangeSize(keys.arraySize + 1);
                }
                GUI.color = Color.red;
                if (GUILayout.Button("-", GUILayout.MinWidth(10), GUILayout.MaxWidth(30)))
                {
                    ChangeSize(keys.arraySize - 1);
                }
                TheEditor.RefuseGui(this);



                EditorGUILayout.EndHorizontal();
                EditorGUI.indentLevel--;


            }
            else if(advValue==false||IsExpand())
            {
             
                reorderable.DoLayoutList();
                
            }





            object[] keyValues = keys.ToEnumerable().Select(element => element.GetJustValue()).ToArray();
                
                
               
               
        
                EditorGUILayout.Space();
                EditorGUILayout.Space();
                if ((keyValues.Distinct().Count()==keyValues.Length) == false)
                {

                  

                    TheEditor.HelpBox("Key duplicate", MessageType.Warning, new GUIStyle() {fixedHeight=30});
                    
                }
                
             
                EditorGUI.indentLevel--;

            

        }


    }
}
