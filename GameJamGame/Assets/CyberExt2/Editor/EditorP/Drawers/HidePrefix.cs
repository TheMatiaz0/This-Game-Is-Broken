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
    [Drawer(typeof(HidePrefixAttribute))]
    public class HidePrefix : IPrefixDrawer
    {
        public void DrawPrefix(GUIContent content, GUIStyle style, CyberAttrribute cyberAttrribute)
        {
            return;
        }
    }
}
