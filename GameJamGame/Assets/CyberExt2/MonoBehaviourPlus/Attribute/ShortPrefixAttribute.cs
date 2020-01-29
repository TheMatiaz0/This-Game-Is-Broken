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
    [AttributeUsage(AttributeTargets.Field)]
    public class ShortPrefixAttribute : CyberAttrribute
    {
       
        public float Width { get; }
        public bool LabelMode { get; }
        public ShortPrefixAttribute(float width, bool labelMode=false)
        {
            Width = width;
            LabelMode = labelMode;
        }
        
    }
}
