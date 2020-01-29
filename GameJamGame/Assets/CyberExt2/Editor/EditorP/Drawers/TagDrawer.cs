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
    [Drawer(typeof(TagAttribute))]
    public class TagDrawer : IDirectlyDrawer
    {
        public void DrawDirectly(SerializedProperty property, CyberAttrribute atribute, GUIContent content, GUIStyle style, FieldInfo field)
        {
            EditorGUILayout.BeginHorizontal();
            TheEditor.DrawPrefix(content, field, style);
            property.stringValue = EditorGUILayout.TagField(property.stringValue);
            EditorGUILayout.EndHorizontal();




        }
    }
}
