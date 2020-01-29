using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cyberevolver;
using Cyberevolver.Unity;
using UnityEngine;
using System.Collections;
namespace Cyberevolver.Unity
{
    public enum CyberObjectMode
    {
        Expandable,
        InOneLine,
        AlwaysExpanded
    }
    [AttributeUsage(AttributeTargets.Class|AttributeTargets.Struct)]
    public class ShowCyberInspectorAttribute : CyberAttrribute
    {
        

        public CyberObjectMode CyberObjectMode { get; }
        public BackgroundMode BackgroundMode { get; }
        public bool NameIn { get; }
        public ShowCyberInspectorAttribute(
            CyberObjectMode cyberObjectMode = CyberObjectMode.Expandable,
            BackgroundMode backgroundMode = BackgroundMode.None,
            bool nameIn=false)
        {
            NameIn = nameIn;
            CyberObjectMode = cyberObjectMode;
            BackgroundMode = backgroundMode;
        }




    }
}
