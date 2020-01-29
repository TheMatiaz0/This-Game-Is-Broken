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
    [Drawer(typeof(SceneReferenceOnlyAttribute))]
    public class SceneReferenceOnlyDrawer : IDirectlyDrawer
    {
        public void DrawDirectly(SerializedProperty property, CyberAttrribute atribute, GUIContent content, GUIStyle style, FieldInfo field)
        {
            EditorGUILayout.BeginHorizontal();
            TheEditor.DrawPrefix(content, field, style);
            EditorGUILayout.ObjectField(property,GUIContent.none);

            if(property.objectReferenceValue!=null&&PrefabUtility.GetPrefabAssetType(property.objectReferenceValue)!=PrefabAssetType.NotAPrefab)
            {
                Debug.LogWarning("Scene refence only"); 
                property.objectReferenceValue = null;
                
            }
            EditorGUILayout.EndHorizontal();
        }
    }
}
