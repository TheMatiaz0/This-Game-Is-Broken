using Cyberevolver.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

namespace Cyberevolver.EditorUnity
{
    [CustomPropertyDrawer(typeof(SerializeTimeSpan))]
    public class TimeSpanDrawer:CyberevolverPropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {

            base.OnGUI(position, property, label);
            int index = 1;
            position.height = base.GetPropertyHeight(property, label);
            EditorGUI.BeginProperty(position, new GUIContent($"{label.text}"), property);
            int start= EditorGUI.indentLevel;
          
            if(property.isExpanded= EditorGUI.Foldout(position,property.isExpanded,label,true))
            {
                position = AddLine(position);
                EditorGUI.indentLevel++;
               
                SerializedProperty precision = property.FindPropertyRelative("precision");
                EditorGUI.PropertyField(position,precision);
                position = AddLine(position);
                if(precision.intValue>5)
                {
                    precision.intValue = 5;
                }
                else if(precision.intValue<=0)
                {
                    precision.intValue = 1;
                }             
             
                AddField("miliseconds");
                AddField("seconds");
                AddField("minutes");
                AddField("hour");
                AddField("day");
                void AddField(string fieldName)
                {

                    if (index > precision.intValue)
                        return;

                        index++;
                    EditorGUI.PropertyField(position, property.FindPropertyRelative(fieldName));
                    position = AddLine(position);

                }
            }
         
            EditorGUI.indentLevel = start;
            EditorGUI.EndProperty();
        }
    }
}