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
using System.Threading;

namespace Cyberevolver.EditorUnity
{
    [Drawer(typeof(IllegalValuesAttribute))]
    public class IllegalValuesDrawer : ILimitDrawer
    {
        public bool IsGoodValue(CyberAttrribute attrribute, object val)
        {
            IllegalValuesAttribute illegalValueAttribute = attrribute as IllegalValuesAttribute;
           foreach(var item in illegalValueAttribute.Values)
            {
                if (item.Equals(val))
                {
                    Debug.LogWarning($"Illegal Value {{{item}}}");  
                    return false;
                    
                }
                    
            }
            return true;
        }
    }
}
