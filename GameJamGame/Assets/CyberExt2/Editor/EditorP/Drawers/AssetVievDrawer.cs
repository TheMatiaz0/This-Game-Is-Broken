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
    [Drawer(typeof(AssetViewAttribute))]
    public class AssetVievDrawer : IDirectlyDrawer
    {

       
     

        public void DrawDirectly(SerializedProperty property, CyberAttrribute atribute, GUIContent content, GUIStyle style, FieldInfo field)
        {

            bool drawExpand = true;
            bool isExpand = false;
            if(CyberEdit.Current.CurrentProp == null)
                return;
            if (CyberEdit.Current.CurrentProp.propertyType != SerializedPropertyType.ObjectReference)
                Debug.LogError($"{nameof(AssetVievDrawer)} should be use only with objectReference type");
            if (CyberEdit.Current.CurrentProp.objectReferenceValue == null)
                drawExpand = true;

          
            EditorGUILayout.BeginHorizontal();
            if (drawExpand&&property.objectReferenceValue!=null)
            {
               
                isExpand = CyberEdit.Current.CurrentProp.isExpanded = EditorGUILayout.Foldout(CyberEdit.Current.CurrentProp.isExpanded, content, true);
                EditorGUI.indentLevel++;
            }
            else
            {
                EditorGUILayout.PrefixLabel(content);
            }
                
            TheEditor.DrawProperty(field,property,true,true);
            EditorGUILayout.EndHorizontal();
   
            if (isExpand) 
            {
                EditorGUILayout.BeginVertical("GroupBox");
                EditorGUI.indentLevel++;
                var editor = Editor.CreateEditor(CyberEdit.Current.CurrentProp.objectReferenceValue);


                editor.OnInspectorGUI();
                EditorGUILayout.EndHorizontal();
                EditorGUI.indentLevel--;
            }
            EditorGUI.indentLevel--;
        }
    }
}
