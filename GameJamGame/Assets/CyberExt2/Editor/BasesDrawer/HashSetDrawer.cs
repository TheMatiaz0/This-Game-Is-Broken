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
    [CustomPropertyDrawer(typeof(BasSerializableHashSet),true)]
    public class HashSetDrawer : PropertyDrawer
    {
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return 0;
        }
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            

            var elements = property.FindPropertyRelative("elements");
            TheEditor.DrawArray
                (property.serializedObject.targetObject.GetType().GetField(property.name, BindingFlags.Public | BindingFlags.NonPublic|BindingFlags.Instance),
                label,elements);

            var asValue = elements.ToEnumerable().Select(item => item.GetJustValue());
               if (asValue.Distinct().Count()!=asValue.Count())
            {
               
                TheEditor.HelpBox("Two identical key", MessageType.Warning, new GUIStyle() { fixedHeight=30});
               
            }

        }

    }
}
