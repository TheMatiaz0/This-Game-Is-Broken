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
    [Drawer(typeof(OnlyRuntimeAttribute))]
    public class OnlyRuntimeDrawer : IConditionsDrawer, IMetaDrawer
    {

        public void DrawBefore(CyberAttrribute cyberAttrribute)
        {
            OnlyRuntimeAttribute atr = cyberAttrribute as OnlyRuntimeAttribute;
            if (Application.isPlaying == false)
                GUI.enabled = false;

        }
        public void DrawAfter(CyberAttrribute atr)
        {

        }

        public bool CanDraw(CyberAttrribute cyberAttrribute)
        {
            OnlyRuntimeAttribute atr = cyberAttrribute as OnlyRuntimeAttribute;
            return atr.ActionIfNotRuntime == ActionIfNotRuntime.Disable || Application.isPlaying;
        }
    }
}
