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
namespace  Cyberevolver.EditorUnity
{
    [Drawer(typeof(StartVerticalAttribute))]
    public class StartVerticalDrawer : IMetaDrawer,IEnderDrawer
    {
      

        public void DrawAfter(CyberAttrribute cyberAttribute)
        {
            
        }


        public void DrawBefore(CyberAttrribute cyberAttribute)
        {
            bool before;
            before = GUI.enabled;
            GUI.enabled = true;
            StartVerticalAttribute attribute = cyberAttribute as StartVerticalAttribute;
          
            TheEditor.BeginVertical(attribute.BackgroundMode);
           
            

            GUI.enabled = before;
        }

        public void DrawEnd(CyberAttrribute cyberAttribute)
        {
            GUILayout.EndVertical();
           
        }

      
    }
}
