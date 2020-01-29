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
    [Drawer(typeof(DropdownByAttribute))]
    public class DropdownByDrawer : IDirectlyDrawer
    {
        public void DrawDirectly(SerializedProperty property, CyberAttrribute attribute, GUIContent content, GUIStyle style, FieldInfo field)
        {
            DropdownByAttribute attr = attribute as DropdownByAttribute;
            TheEditor.DrawPropertyAsDropdownWithFixValue(
                property,
                field,
                content,
                style,
                GetValue(attr.ValueGetter).OfType<object>().ToArray(),
                (i) => property.SetValue(i),
                property.GetJustValue(),
                (item, index) =>item?.ToString()??"null",
                true,
                false);
        }
        private IEnumerable GetValue(string name)
        {
            Type type = CyberEdit.Current.Target.GetType();
            var bindingFlags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static;
            var value = type.GetField(name, bindingFlags)?
                .GetValue(CyberEdit.Current.Target) as IEnumerable;
            value=value ?? type.GetProperty(name, bindingFlags)?
                .GetValue(CyberEdit.Current.Target) as IEnumerable;
            value = value ?? type.GetMethod(name, bindingFlags)?
                .Invoke(CyberEdit.Current.Target, new object[] { }) as IEnumerable;
            return value;

        }
    }
}
