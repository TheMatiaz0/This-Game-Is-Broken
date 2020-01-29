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
    [Drawer(typeof(DropdownAttribute))]
    public class DropdownDrawer : IDirectlyDrawer
    {

        public void DrawDirectly(SerializedProperty property, CyberAttrribute atribute, GUIContent content, GUIStyle style, FieldInfo field)
        {
            DropdownAttribute atr = atribute as DropdownAttribute;
         


            TheEditor.DrawPropertyAsDropdownWithFixValue(property, field, content, style, atr.Values, (i) => property.SetValue(i), property.GetJustValue(),
                (item, index) => atr.Names[index],true,atr.ShowAsName);



        }
    }
}
