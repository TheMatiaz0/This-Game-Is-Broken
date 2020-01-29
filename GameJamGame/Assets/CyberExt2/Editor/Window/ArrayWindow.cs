using UnityEngine;
using UnityEditor;
using System.Reflection;

namespace Cyberevolver.EditorUnity
{
    public class ArrayWindow : EditorWindow
    {

      
        private SerializedObject serializedObject;
        private FieldInfo field;
        private GUIContent content;
        private SerializedProperty prop;
        private int zone = 0;
        public static void Open(UnityEngine.Object obj,FieldInfo field,GUIContent content)
        {

            ArrayWindow window = EditorWindow.GetWindow<ArrayWindow>(field.Name);
            window.field = field;
            window.content = content;
            window.serializedObject = new SerializedObject( obj);
            window.prop = window.serializedObject.FindProperty(field.Name);

        }
        private void OnGUI()
        {


            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.BeginVertical("box", GUILayout.MaxWidth(120), GUILayout.MinWidth(40), GUILayout.ExpandHeight(true));




            DrawSlideBar();
            EditorGUILayout.EndVertical();
            EditorGUILayout.BeginVertical("box",GUILayout.ExpandHeight(true));
            if (prop.arraySize > zone)
                EditorGUILayout.PropertyField(prop.GetArrayElementAtIndex(zone), GUIContent.none);
            EditorGUILayout.EndVertical();
            EditorGUILayout.EndHorizontal();


          
            serializedObject.ApplyModifiedProperties();
        }
        private void DrawSlideBar()
        {
            prop.arraySize = EditorGUILayout.IntField("Size", prop.arraySize);
            for (int i = 0; i < prop.arraySize; i++)
            {
                var nameV = prop.GetArrayElementAtIndex(i).FindPropertyRelative("name");
                string addingText = "";
                if(nameV!=null&&nameV.propertyType==SerializedPropertyType.String&&nameV.stringValue!=string.Empty)
                {
                    addingText += $"({nameV.stringValue})";
                }
                string text = $"{i} {addingText}";
                if (GUILayout.Button(text))
                {
                    zone = i;
                }
            }
        }
    }
}
