using Cyberevolver.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEditor.UI;

namespace Cyberevolver.EditorUnity
{
    [CustomEditor(typeof(BetterInputField))]
    public class BetterInputFieldEditor : InputFieldEditor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            EditorGUILayout.PropertyField(serializedObject.FindProperty("isFocusedUpdate"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("isNotFocusedUpdate"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("hasOnceFocus"));

            SerializedProperty checkForInput;
            EditorGUILayout.PropertyField(checkForInput = serializedObject.FindProperty("checkForInput"));
            if (checkForInput.boolValue == true)
            {
                EditorGUILayout.PropertyField(serializedObject.FindProperty("searchString"));
                EditorGUILayout.PropertyField(serializedObject.FindProperty("textIsCorrect"));
            }
            serializedObject.ApplyModifiedProperties();
        }
    }
}
