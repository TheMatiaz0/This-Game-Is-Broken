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
    [Drawer(typeof(HelpBoxAttribute))]
    public class HelpBoxDrawer : IMetaDrawer
    {

        public void DrawBefore(CyberAttrribute atr)
        {
            var attribute = atr as HelpBoxAttribute;
            TheEditor.HelpBox(attribute.Text, attribute.MessageType,
                new GUIStyle() { fixedHeight = attribute.Height, fixedWidth = attribute.Width });


        }
        public void DrawAfter(CyberAttrribute atr)
        {

        }
    }
}
