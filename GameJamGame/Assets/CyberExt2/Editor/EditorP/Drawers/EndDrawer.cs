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
    [Drawer(typeof(EndAttribute))]
    public class EndDrawer : IMetaDrawer
    {
        public  void DrawAfter(CyberAttrribute atr)
        {
           
        }

        public  void DrawBefore(CyberAttrribute atr)
        {

            TheEditor.Pop();

        }
    }
}
