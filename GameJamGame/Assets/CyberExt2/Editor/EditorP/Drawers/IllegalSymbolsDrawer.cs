using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cyberevolver;
using Cyberevolver.Unity;
using UnityEngine;
using System.Collections;
using UnityEditor;
namespace Cyberevolver.EditorUnity
{
    [Drawer(typeof(IllegalSymbolsAttribute))]
    public class IllegalSymbolsDrawer : IMetaDrawer
    {
        public void DrawAfter(CyberAttrribute cyberAttributer)
        {
            IllegalSymbolsAttribute atr = cyberAttributer as IllegalSymbolsAttribute;
            var prop = CyberEdit.Current.CurrentProp;
            var str = prop.stringValue;
            foreach(string symbol in atr.Symbols)
            {
                string before = str;
                str= str.Replace(symbol, "");
                if(str.Length!=before.Length)
                {
                    Debug.LogWarning("Illegal symbol");
                }
            }
            prop.stringValue = str;
        
            
        }

        public void DrawBefore(CyberAttrribute cyberAttributer)
        {
            
        }
    }
}
