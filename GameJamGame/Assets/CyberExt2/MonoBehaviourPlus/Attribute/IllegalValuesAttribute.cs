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
    [AttributeUsage(AttributeTargets.Field)]
    public class IllegalValuesAttribute : CyberAttrribute
    {
      

        public object[] Values { get; }
        public IllegalValuesAttribute(object first,params object[] values)
        {
            Values = values.Append(first).ToArray();
        }
    }
}
