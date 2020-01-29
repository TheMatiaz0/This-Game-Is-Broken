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
    [Drawer(typeof(ShowGroupIfAttribute))]
    public class ShowGroupIfDrawer : IGroupConditionsDrawer
    {
        public bool CanDraw(CyberAttrribute cyberAttrribute)
        {
            ShowGroupIfAttribute atr = cyberAttrribute as ShowGroupIfAttribute;
            return atr.Equaler.CheckEquals(atr.Value, CyberEdit.Current.GetPropByName(atr.Prop).GetJustValue());
        }
    }
}
