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
    [AttributeUsage(AttributeTargets.Field|AttributeTargets.Method)]
    public class StartHorizontalAttribute : CyberAttrribute
    {
      

        public BackgroundMode BackgroundMode { get; }
        public float RightPush { get; set; } = 0;
        public string Name { get; }
        public StartHorizontalAttribute(string name = null, BackgroundMode mode = BackgroundMode.None)
        {
            BackgroundMode = mode;
            Name = name;
        }
        public StartHorizontalAttribute(BackgroundMode mode,string name=null)
            : this(name, mode) { }

    }
}
