using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cyberevolver;
using Cyberevolver.Unity;
using UnityEngine;
using UnityEditor;
using System.Collections;
namespace Cyberevolver.EditorUnity
{
    [Drawer(typeof(IndentLvAttribute))]
    public class IndentLvDrawer : IMetaDrawer
    {
        public void DrawAfter(CyberAttrribute atr)
        {
            if ((atr as IndentLvAttribute).OneShoot)
                EditorGUI.indentLevel -= (atr as IndentLvAttribute).Quantity;
        }
        public void DrawBefore(CyberAttrribute atr)
        {
           
            EditorGUI.indentLevel += (atr as IndentLvAttribute).Quantity;
        }
    }
}

