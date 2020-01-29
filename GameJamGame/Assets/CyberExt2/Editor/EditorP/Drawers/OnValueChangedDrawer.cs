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
    [Drawer(typeof(OnValueChangedAttribute))]
    public class OnValueChangedDrawer : IMetaDrawer
    {
        public void DrawAfter(CyberAttrribute cyberAttributer)
        {
            
            OnValueChangedAttribute atr = cyberAttributer as OnValueChangedAttribute;
            if (EditorGUI.EndChangeCheck())
            {
                CyberEdit.Current.SerializedObject.ApplyModifiedProperties();
                var method = CyberEdit.Current.Target.GetType().GetMethod(atr.Call,
                      BindingFlags.Public | BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Static);
                object[] parameters;
                object caller;
                if (method.GetParameters().Length > 0)
                    parameters = new object[] { CyberEdit.Current.CurrentField.GetValue(CyberEdit.Current.Target) };
                else
                    parameters = new object[0];
                if (method.IsStatic)
                    caller = null;
                else
                    caller = CyberEdit.Current.Target;
                method.Invoke(caller, parameters);

            }
        }

        public void DrawBefore(CyberAttrribute cyberAttributer)
        {
            EditorGUI.BeginChangeCheck();
        }
    }
}
