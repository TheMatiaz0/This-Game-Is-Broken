using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cyberevolver;
using Cyberevolver.Unity;
using UnityEngine;
using System.Collections;
using UnityEngine.Events;

namespace Cyberevolver.Unity
{
    
    [AttributeUsage(AttributeTargets.Method|AttributeTargets.Field)]
    public class ButtonAttribute : ColorAttribute
    {
       

        public string Text { get; } 
        public UnityEventCallState WhenCanPress { get; }
        public float Height { get; }
        public string Method { get; set; }
      
        
      

        public ButtonAttribute(string text="",AColor color=AColor.None, 
            UnityEventCallState whenCanPress=UnityEventCallState.EditorAndRuntime,
            UISize size=UISize.SoSmall)
            :base(color)
        {

            Height = (int)size;
            Text = text ?? throw new ArgumentNullException(nameof(text));
            WhenCanPress = whenCanPress;
        }
        public ButtonAttribute( UnityEventCallState whenCanPut, string text = "")
            : this(text, AColor.None,whenCanPut) { }
        public ButtonAttribute(UISize size)
            : this("", AColor.None,UnityEventCallState.EditorAndRuntime, size) { }



    }
}
