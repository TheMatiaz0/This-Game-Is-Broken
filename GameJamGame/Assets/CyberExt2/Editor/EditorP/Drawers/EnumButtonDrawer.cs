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
    [Drawer(typeof(EnumButtonsAttribute))]
    public class EnumButtonDrawer : IDirectlyDrawer
    {
        public void DrawDirectly(SerializedProperty property, CyberAttrribute atrribute, GUIContent content, GUIStyle style, FieldInfo field)
        {
            EditorGUILayout.BeginHorizontal();
            TheEditor.DrawPrefix(content, field, style);
            foreach(var item in Enum.GetValues(CyberEdit.Current.CurrentField.FieldType))
            {
                int iVal = Convert.ToInt32(item);
                if (GUILayout.Toolbar(iVal == property.intValue ? 0 : 1, new GUIContent[] { new GUIContent(item.ToString()) })==0)
                {
                    property.intValue = iVal;
                }
            }
            EditorGUILayout.EndHorizontal();
        }
    }
}
