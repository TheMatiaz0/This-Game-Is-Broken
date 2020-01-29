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
using System.Reflection;

namespace Cyberevolver.EditorUnity
{
    [Drawer(typeof(HorizontalArrayAttribute))]
    public class HorizontalDrawer : IDirectlyDrawer
    {
      
        public void DrawDirectly(SerializedProperty property, CyberAttrribute atribute, GUIContent content, GUIStyle style,FieldInfo field)
        {


            HorizontalArrayAttribute atr = atribute as HorizontalArrayAttribute;


           if( property.isExpanded = EditorGUILayout.Foldout(property.isExpanded, content,true))
            {
                EditorGUI.indentLevel++;
                TheEditor.DrawSize(field,CyberEdit.Current.CurrentProp);
               
                EditorGUILayout.BeginVertical("GroupBox");
                
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.BeginVertical();
                EditorGUILayout.BeginHorizontal();
                int before = EditorGUI.indentLevel;
                EditorGUI.indentLevel = 0;
                int index = 0;

                foreach (SerializedProperty pr in property.ToEnumerable())
                {
                    bool next = index != 0 && index % atr.ElementAtOneWide == 0;
                    if(next)
                        EditorGUILayout.EndHorizontal();

                    if (next)
                        EditorGUILayout.BeginHorizontal();
                   
                    EditorGUILayout.PropertyField(pr, GUIContent.none,GUILayout.MinWidth( atr.MinWith));
                   
                    index++;
                }
                if(index % atr.ElementAtOneWide != 0)
                    for (int x = 0; x < atr.ElementAtOneWide - index % atr.ElementAtOneWide; x++)
                    {
                        EditorGUILayout.LabelField("", GUILayout.MinWidth( atr.MinWith));
                    }
                EditorGUI.indentLevel = before;
                EditorGUILayout.EndHorizontal();
                EditorGUILayout.EndVertical();
                EditorGUILayout.EndHorizontal(); 
            
                
               
                EditorGUILayout.EndVertical();
                
                EditorGUI.indentLevel--;
            }

           

        }
    }
}
