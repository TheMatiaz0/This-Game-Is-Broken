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
    [Drawer(typeof(EnumFlagsAttribute))]
    public class EnumFlagDrawer : IDirectlyDrawer
    {
        public void DrawDirectly(SerializedProperty property, CyberAttrribute atrribute, GUIContent content, GUIStyle style, FieldInfo field)
        {
            EditorGUILayout.BeginHorizontal();
            TheEditor.DrawPrefix(content, field, style);
            property.intValue = EditorGUILayout.MaskField(GUIContent.none, property.intValue, property.enumNames);

            EditorGUILayout.EndHorizontal();
        }
        public Enum GetEnum(int value,Type type)
        {
            return Enum.GetValues(type).OfType<Enum>().FirstOrDefault(item => Convert.ToInt32(item).Equals(value))??Enum.GetValues(type).OfType<Enum>().First();
        }
    }
}
