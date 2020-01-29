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
    [CyberAttributeUsage(LegalTypeFlags.NonGeneric)]
    [AttributeUsage(AttributeTargets.Field)]
    public class ResetButtonAttribute : CyberAttrribute
    {
      

        public object Value { get; }
        public ResetButtonAttribute(object value)
        {
            Value = value ?? throw new ArgumentNullException(nameof(value));
        }

    }
}
