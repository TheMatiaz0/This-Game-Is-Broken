using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using Cyberevolver;
using Cyberevolver.Unity;
using UnityEngine;
using System.Reflection;

namespace Cyberevolver.EditorUnity
{
    [CustomEditor(typeof(StaticMethodInvoker))]
    public class StaticMethodInvokeEditor:Editor
    {
        private SerializedProperty methodP;
        private SerializedProperty argsP;
        private SerializedProperty referenceP;
        private void OnEnable()
        {
             methodP = serializedObject.FindProperty("method");
             argsP = serializedObject.FindProperty("args");
            referenceP = serializedObject.FindProperty("reference");
        }
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();                     
            Type type= StaticMethodDrawer.GetCanDrawTypes().FirstOrDefault(item => item.FullName == methodP.FindPropertyRelative("_TypeName").stringValue);
            if (type == null)
                return;
            MethodInfo methodInf = type.GetMethod(methodP.FindPropertyRelative("_MethodName").stringValue);
            if (methodInf == null||methodInf.GetParameters().Length ==0 )
                return;
            else
            {
                ParameterInfo param = methodInf.GetParameters()[0];
                Type paramType = param.ParameterType;
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.PrefixLabel(new GUIContent(param.Name));
                (argsP.stringValue, referenceP.objectReferenceValue) = TheEditor.GeneralField(argsP.stringValue, referenceP.objectReferenceValue, paramType);

                EditorGUILayout.EndHorizontal();
                   
               
            }
            serializedObject.ApplyModifiedProperties();
        }
    }
}