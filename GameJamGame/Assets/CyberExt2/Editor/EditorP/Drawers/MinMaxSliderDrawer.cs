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
    [Drawer(typeof(MinMaxSliderAttribute))]
    public class MinMaxSliderDrawer : IDirectlyDrawer
    {

    

        public void DrawDirectly(SerializedProperty property, CyberAttrribute cyberAttribute, GUIContent content, GUIStyle style, FieldInfo field)
        {
            MinMaxSliderAttribute atr = cyberAttribute as MinMaxSliderAttribute;
            EditorGUILayout.BeginHorizontal();
            TheEditor.DrawPrefix(content, field, style);
            float min, max;
            SerializedProperty pVect = null;

           if(property.propertyType==SerializedPropertyType.Generic)
            {
                pVect = property.FindPropertyRelative("val");

                min = pVect.vector2Value.x;
                max = pVect.vector2Value.y;
            }


            else if(property.propertyType==SerializedPropertyType.Vector2)
            {
                 min = property.vector2Value.x;
                 max = property.vector2Value.y;
            }
            else
            {
                min = property.vector2IntValue.x;
                max = property.vector2IntValue.y;
            }


            min = EditorGUILayout.FloatField(min, GUILayout.MinWidth(15),GUILayout.MaxWidth(70));
            EditorGUILayout.MinMaxSlider(ref min, ref max, atr.Min, atr.Max);
            max = EditorGUILayout.FloatField(max, GUILayout.MinWidth(15), GUILayout.MaxWidth(70));
            if (property.propertyType == SerializedPropertyType.Vector2)
                property.vector2Value = new Vector2(min, max);
            else if(property.propertyType==SerializedPropertyType.Vector2Int)
                property.vector2IntValue = new Vector2Int((int)min,(int) max);
            else if(property.propertyType==SerializedPropertyType.Generic)
            {
                pVect.vector2Value = new Vector2(min, max);
            }
            EditorGUILayout.EndHorizontal();

        }
    }
}
