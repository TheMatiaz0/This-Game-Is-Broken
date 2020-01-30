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
namespace Cyberevolver.EditorUnity
{
    [CustomPropertyDrawer(typeof(Range))]
    public class RangeDrawer : PropertyDrawer
    {

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return 0;
        }
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUILayout.BeginHorizontal();

            var vectProp = property.FindPropertyRelative("val");
            EditorGUILayout.PropertyField(vectProp);
            if (vectProp.vector2Value.y < vectProp.vector2Value.x)
                vectProp.vector2Value = new Vector2(vectProp.vector2Value.y, vectProp.vector2Value.y);


            EditorGUILayout.EndHorizontal();
        }
    }
}
