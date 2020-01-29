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
    [Drawer(typeof(AssetOnlyAttribute))]
    public class AssetOnlyDrawer : IDirectlyDrawer
    {
        public void DrawDirectly(SerializedProperty property, CyberAttrribute atribute, GUIContent content, GUIStyle style, FieldInfo field)
        {
            EditorGUILayout.BeginHorizontal();
            TheEditor.DrawPrefix(content, field, style);
            property.objectReferenceValue = EditorGUILayout.ObjectField(GUIContent.none, property.objectReferenceValue,  CyberEdit.Current.GetFieldByName(property.name).FieldType, false);
            EditorGUILayout.EndHorizontal();

        }
    }
}
