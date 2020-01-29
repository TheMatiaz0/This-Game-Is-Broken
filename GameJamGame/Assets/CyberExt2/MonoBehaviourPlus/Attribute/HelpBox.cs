using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cyberevolver;
using Cyberevolver.Unity;
using UnityEngine;
using System.Collections;

public enum MessageType// identical to UnityEditor version.I cannot use that one becuase UnityEditor won't be compiled info final game.
{

    None = 0,
    Info = 1,
    Warning = 2,
    Error = 3
}

namespace Cyberevolver.Unity
{
   
    [AttributeUsage(AttributeTargets.Field)]
    public class HelpBoxAttribute : CyberAttrribute
    {
        

        public string Text { get; }
        public MessageType MessageType { get; }
       
        public float Width { get; set; }
        public float Height { get; }
        public HelpBoxAttribute(string text, MessageType messageType=MessageType.Info, UISize size=UISize.Default)
         : this(text, messageType, (int)size) { }

        public HelpBoxAttribute(string text, MessageType messageType=MessageType.Info, float height=0)
        {
            Text = text ?? throw new ArgumentNullException(nameof(text));
            MessageType = messageType;
            Height = height;
        }
     

    }
}
