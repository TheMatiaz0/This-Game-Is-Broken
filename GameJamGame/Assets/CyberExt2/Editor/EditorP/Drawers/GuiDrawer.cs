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
    [Drawer(typeof(CustomGuiAttribute))]
    public class GuiDrawer : IStyleDrawer
    {
        public void ChangeGuiStyle(CyberAttrribute attrribute, ref GUIStyle style,ref string customName)
        {
            var fatr = (attrribute as CustomGuiAttribute);
            style =fatr.GUIStyle;
            customName = fatr.Label ?? customName;
        }

     
        
    }
}
