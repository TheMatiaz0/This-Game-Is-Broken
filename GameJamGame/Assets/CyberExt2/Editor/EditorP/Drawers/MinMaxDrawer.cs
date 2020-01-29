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
    [Drawer(typeof(MinMaxRangeAttribute))]
    public class MinMaxDrawer : IDirectlyDrawer
    {
        public void DrawDirectly(SerializedProperty property, CyberAttrribute atribute, GUIContent content, GUIStyle style, FieldInfo field)
        {
            MinMaxRangeAttribute atr = atribute as MinMaxRangeAttribute;


            EditorGUILayout.BeginHorizontal();
            TheEditor.DrawPrefix(content, field, style);
            property.SetValue(EditorGUILayout.Slider((float)Convert.ChangeType(property.GetJustValue(), typeof(float)), atr.Min, atr.Max));
            EditorGUILayout.EndHorizontal();
        }

      

    }
}
