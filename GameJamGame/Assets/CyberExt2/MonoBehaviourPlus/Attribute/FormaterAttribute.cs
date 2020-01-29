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
    
    public class FormaterAttribute : ColorAttribute
    {
      
        public FontStyle FontStyle { get;  }
        public int FontSize { get;  }
        public bool RichText { get; set; }
        public float Height { get;  } = 20;
        public float Width { get; set; } = 0;
    

        public GUIStyle GUIStyle
        {
            get
            {
                GUIStyle style = new GUIStyle
                {
                    fontStyle = FontStyle,
                    fontSize = FontSize,
                    richText = RichText,
                    fixedHeight = Height,
                    fixedWidth = Width
                };
                if (CustomColor)
                    style.normal.textColor = CurColor;
              
                return style;
            }
        }

        public string Label { get; set; }
        private FormaterAttribute(string label, FontStyle fontStyle, Color color, int fontSize, float height)
            :base(color)

        {
            FontStyle = fontStyle;
            FontSize = fontSize;
            Height = height;
            Label = label ?? throw new ArgumentNullException(nameof(label));
          
         
         
        }
        public FormaterAttribute(string label, FontStyle fontStyle = FontStyle.Normal, AColor color=AColor.None,  UISize size=UISize.Default, int fontSize = 0)
            :this(label,fontStyle,GetColorByName(color).color,fontSize,(int)size)//i see that repeating, it is only way
        {
            if (color == AColor.None)
                CustomColor = false;
        }
       
        public FormaterAttribute(string label, FontStyle fontStyle, string rgb, UISize size = UISize.Default, int fontSize = 0)
           : this(label, fontStyle, ColorExtension.ParseClasic(rgb), fontSize, (int)size) { }
      
        public FormaterAttribute(string label, string rgb, UISize size = UISize.Default, int fontSize = 0)
          : this(label, FontStyle.Normal,rgb:  rgb, size, fontSize) { }
       



    }
}
