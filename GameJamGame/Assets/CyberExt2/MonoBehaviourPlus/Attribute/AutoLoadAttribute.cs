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
    [CyberAttributeUsage(LegalTypeFlags.ObjectReference)]
    [AttributeUsage(AttributeTargets.Field)]
    public class AutoLoadAttribute : CyberAttrribute
    {
        public AutoLoadAttribute(bool withChildren=false, bool angryPutIfNull=true)
        {
            AngryPutIfNull = angryPutIfNull;
            WithChildren = withChildren;
        }

        public bool AngryPutIfNull { get; }
        public bool WithChildren { get; }
        
    }
}
