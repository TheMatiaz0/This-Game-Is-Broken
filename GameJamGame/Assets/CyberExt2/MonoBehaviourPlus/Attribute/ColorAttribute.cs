using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cyberevolver;
using Cyberevolver.Unity;
using UnityEngine;
using System.Collections;
using static Cyberevolver.Unity.AColor;
public abstract class ColorAttribute : CyberAttrribute
{
   
    public bool CustomColor { get; protected set; } = false;
    public Color CurColor { get; private set; }
    public AColor MainColor
    {
        get => throw new NotImplementedException();
        set
        {
            var color = GetColorByName(value);
            if(color.isCustom)
            {
                CurColor = color.color;
            }
        }
    }
    public ColorAttribute(string rgb)
    {
        CurColor = ColorExtension.ParseClasic(rgb);
        CustomColor = true;

    }
    protected ColorAttribute()
    {

    }
    protected ColorAttribute(Color color)
    {
        CurColor = color;
        CustomColor = true;
    }
    public ColorAttribute(AColor color)
    {
        (CurColor, CustomColor) = GetColorByName(color);   
    }
 
    private static Dictionary<AColor, Color> dic = new Dictionary<AColor, Color>()
    {
        [Red] = Color.red,
        [Blue] = Color.blue,
        [Yellow] = Color.yellow,
        [Black] = Color.black,
        [Green] = Color.green,
        [Grey] = Color.grey,
        [White] = Color.white,
        [Clear] = Color.clear,
        [Cyan] = Color.cyan
    };
    public static Color? GetColorByNameOut(AColor aColor)
    {
        var color = GetColorByName(aColor);
        if (color.isCustom==false)
            return null;
        else return color.color;



    }
    public static (Color color,bool isCustom) GetColorByName(AColor name)
    {
        if (dic.ContainsKey(name) == false)
            return (default, false);
        else
        {
            return (dic[name], true);
        }
       
    }
}