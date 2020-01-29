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
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Method, AllowMultiple = true)]
    public class SeparatorAttribute : FormaterAttribute
    {
        public SeparatorAttribute(string label, string rgb, UISize size = UISize.Default, int fontSize = 0) : base(label, rgb, size, fontSize)
        {
        }

        public SeparatorAttribute(string label="", FontStyle fontStyle = FontStyle.Normal, AColor color = AColor.None, UISize size = UISize.Default, int fontSize = 0) : base(label, fontStyle, color, size, fontSize)
        {
        }

        public SeparatorAttribute(string label, FontStyle fontStyle, string rgb, UISize size = UISize.Default, int fontSize = 0) : base(label, fontStyle, rgb, size, fontSize)
        {
        }
    }
}
