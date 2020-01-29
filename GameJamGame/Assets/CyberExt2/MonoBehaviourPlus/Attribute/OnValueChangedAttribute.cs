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
    public class OnValueChangedAttribute : CyberAttrribute
    {
      

        public string Call { get; }
        public OnValueChangedAttribute(string call)
        {
            Call = call ?? throw new ArgumentNullException(nameof(call));
        }
    }
}
