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
using UnityEngine.Events;
using System.Reflection;

namespace Cyberevolver.EditorUnity
{
    [Drawer(typeof(ButtonAttribute))]
    public class ButtonDrawer : IMethodDrawer,IMetaDrawer
    {
        public void DrawAfter(CyberAttrribute cyberAttributer)
        {
            if (CyberEdit.Current.CurrentInspectedMember !=null)
            {
                ButtonAttribute attribute = cyberAttributer as ButtonAttribute;
                var type = CyberEdit.Current.GetFinalTargetType();
                DrawMethod(type.GetMethod(attribute.Method, BindingFlags.Public | BindingFlags.Instance|BindingFlags.NonPublic|BindingFlags.Static), attribute);
            }
            EditorGUILayout.EndHorizontal();
          
        }

        public void DrawBefore(CyberAttrribute cyberAttributer)
        {
            EditorGUILayout.BeginHorizontal();
        }

        public void DrawMethod(MethodInfo method, CyberAttrribute cyberAttrribute)
        {
           
            object locker = new object();
            if (method == null)
                throw new ArgumentNullException(nameof(method));
            if (cyberAttrribute == null)
                throw new ArgumentNullException(nameof(method));

         
          
          
            ButtonAttribute button = cyberAttrribute as ButtonAttribute;

            TheEditor.PrepareToRefuseGui(locker);
            if (button.CustomColor)
                GUI.color = button.CurColor;
        
            
                

          

          
            if ((Application.isPlaying == false && button.WhenCanPress == UnityEventCallState.RuntimeOnly)
                || (button.WhenCanPress == UnityEventCallState.Off))
            {

                GUI.enabled = false;
            }

            
            string text = button.Text;
            if (string.IsNullOrEmpty(text))
                text = method.Name;
            if (GUILayout.Button(text,GUILayout.Height(button.Height)))
            {
                if (method.IsStatic == false)
                    method.Invoke(CyberEdit.Current.Target, null);
                else
                    method.Invoke(null, null);
            }
            TheEditor.RefuseGui(locker);
           
        }

        
    }
}
