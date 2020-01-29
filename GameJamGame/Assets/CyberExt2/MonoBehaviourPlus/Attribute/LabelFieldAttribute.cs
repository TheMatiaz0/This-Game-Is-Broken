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
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Method)]
    public class LabelFieldAttribute : FormaterAttribute
    {
        public LabelFieldAttribute(string label, string rgb, int fontSize = 0) : base(label, rgb, UISize.Default, fontSize)
        {
        }

        public LabelFieldAttribute(string label, FontStyle fontStyle = FontStyle.Normal, AColor color = AColor.None, int fontSize = 0) : base(label, fontStyle, color, UISize.Default, fontSize)
        {
        }

        public LabelFieldAttribute(string label, FontStyle fontStyle, string rgb,  int fontSize = 0) : base(label, fontStyle, rgb, UISize.Default, fontSize)
        {
        }

        public bool IgnoreIndentLv { get; set; } = false;
      
       
      
    }
}
