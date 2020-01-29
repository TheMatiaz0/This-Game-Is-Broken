using Cyberevolver.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace Cyberevolver.EditorUnity
{
    [CustomPropertyDrawer(typeof(Cyberevolver.Unity.StaticMethodReference))]
    public class StaticMethodDrawer : PropertyDrawer
    {
        public static IReadOnlyCollection<Type> GetCanDrawTypes() => types;
        private static Type[] types;
        private string typeName;
        private string methodName;
        [InitializeOnLoadMethod]
        public static void Init()
        {
            types = (from type in AppDomain.CurrentDomain.GetAssemblies().SelectMany(item => item.GetTypes())
                     where type.GetCustomAttributes(true).Any(item => item.GetType() == typeof(StaticMethodContainerAttribute))
                     select type).ToArray();
        }
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            Rect startPos = position;
            EditorGUI.BeginProperty(position, label, property);
            SerializedProperty typeP = property.FindPropertyRelative("_TypeName");
            SerializedProperty methodP = property.FindPropertyRelative("_MethodName");
            if (typeName != null)
            {
                typeP.stringValue = typeName;

                typeName = null;
            }
            if (methodName != null)
            {
                methodP.stringValue = methodName;
                methodName = null;
            }
            position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);
            position.width /= 2;

            Type currentType = types.FirstOrDefault(item => item.FullName == typeP.stringValue);
            string shortTypeName = currentType?.Name ?? "null";
            if (EditorGUI.DropdownButton(position, new GUIContent(shortTypeName), FocusType.Passive))
            {
                GenericMenu menu = new GenericMenu();
                foreach (var item in types)
                {
                    menu.AddItem(new GUIContent(item.Name), false, OnTypeSelect, item);
                }
                menu.ShowAsContext();

            }
            position.x += position.width;
            if (EditorGUI.DropdownButton(position, new GUIContent(methodP.stringValue ?? "null"), FocusType.Passive) && currentType != null)
            {
                GenericMenu menu = new GenericMenu();
                foreach (MethodInfo item in currentType.GetMethods().Where(item => item.IsStatic && item.GetParameters().Length <= 1))
                {
                    menu.AddItem(new GUIContent(item.Name), false, OnMethodSelect, item);
                }
                menu.ShowAsContext();
            }

            EditorGUI.EndProperty();
        }
        private void OnTypeSelect(object arg)
        {
            Type type = (Type)arg;
            typeName = type.FullName;
        }
        private void OnMethodSelect(object arg)
        {
            MethodInfo methodInfo = (MethodInfo)arg;
            methodName = methodInfo.Name;
        }
    }

}
