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
    [Drawer(typeof(WindowArrayAttribute))]
    public class WindowArrayDrawer : IDirectlyDrawer
    {
        public void DrawDirectly(SerializedProperty property, CyberAttrribute atribute, GUIContent content, GUIStyle style, FieldInfo field)
        {
            content.text += $" {{size:{property.arraySize}}}";
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.PrefixLabel(content, new GUIStyle(), style);
            if(GUILayout.Button("Open"))
            {
                ArrayWindow.Open(CyberEdit.Current.Target,field,content);
            }
            EditorGUILayout.EndHorizontal();
        }
    }
}
