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
    [AttributeUsage(AttributeTargets.Class|AttributeTargets.Struct,AllowMultiple =true)]
    public class CustomBackgrounGroupAttribute : GroupModiferAttribute
    {
        public CustomBackgrounGroupAttribute( string folder, BackgroundMode backgroundMode,bool alwaysSimpleExpand=false) :base(folder)
        {
            BackgroundMode = backgroundMode;
            AlwaysSimpleExpand = alwaysSimpleExpand;
          
        }
        public bool AlwaysSimpleExpand { get; }

        public BackgroundMode BackgroundMode { get; }
       

    }
}
