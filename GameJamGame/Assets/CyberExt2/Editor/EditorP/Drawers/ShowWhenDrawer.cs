using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using Cyberevolver.Unity;
using System.Reflection;
namespace Cyberevolver.EditorUnity
{
    [Drawer(typeof(Cyberevolver.Unity.ShowWhenAttribute))]
    public class ShowWhenDrawer :IConditionsDrawer
    {

       
       
      
        public  bool CanDraw( CyberAttrribute cyberAttrribute)
        {

            ShowWhenAttribute atr = cyberAttrribute as ShowWhenAttribute;          
            return TheEditor.CheckEquals(CyberEdit.Current.CurrentProp.serializedObject.FindProperty(atr.Prop), atr.Value, atr.Equaler);
          
           
            
            
        }


    }
}
