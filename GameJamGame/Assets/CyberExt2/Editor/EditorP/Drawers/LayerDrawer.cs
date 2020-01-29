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
    [Drawer(typeof(LayerAttribute))]
    public class LayerDrawer : IDirectlyDrawer
    {
        public void DrawDirectly(SerializedProperty property, CyberAttrribute atribute, GUIContent content, GUIStyle style, FieldInfo field)
        {

            if (UnityEditorInternal.InternalEditorUtility.layers.Contains(property.stringValue) == false)
                property.stringValue = UnityEditorInternal.InternalEditorUtility.layers[0];
            TheEditor.DrawPropertyAsDropdown(property, field, content, style, UnityEditorInternal.InternalEditorUtility.layers,
                (item) => property.stringValue = item?.ToString()??"null", null, true);
        }
    }
}
