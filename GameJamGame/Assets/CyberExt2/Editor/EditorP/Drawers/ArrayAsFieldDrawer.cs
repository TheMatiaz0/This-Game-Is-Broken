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
    [Drawer(typeof(ArrayAsFieldAttribute))]
    public class ArrayAsFieldDrawer : IDirectlyDrawer
    {
        public void DrawDirectly(SerializedProperty property, CyberAttrribute attribute, GUIContent content, GUIStyle style, FieldInfo field)
        {
            ArrayAsFieldAttribute atr = attribute as ArrayAsFieldAttribute;
            property.arraySize = atr?.Names.Length ?? 1;
           TheEditor.DrawArray(field, GUIContent.none, property, false,false,false, atr.Names ?? new string[] { "_NoName_" });
        }
    }
}
