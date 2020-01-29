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
    [Drawer(typeof(CustomBackgrounGroupAttribute))]
    public class CustomBackgrounGroupDrawer : IGroupModifer
    {
        public void BeforeGroup(CyberAttrribute cyberAttribute)
        {
            CustomBackgrounGroupAttribute attribute = cyberAttribute as CustomBackgrounGroupAttribute;
            TheEditor.BeginHorizontal(attribute.BackgroundMode);   
        }
        public void AfterGroup(CyberAttrribute cyberAttribute)
        {
            EditorGUILayout.EndHorizontal();
        }

        
    }
}
