using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
namespace Cyberevolver.Unity
{
    public class StaticMethodInvoker:MonoBehaviourPlus
    {
        [SerializeField]
        private StaticMethodReference method = null;
        [HideInInspector]
        [SerializeField]
        private string args = null;
        [HideInInspector]
        [SerializeField]
        private UnityEngine.Object reference = null;      
       
        public void InvokeStaticMethod()
        {
            if (method.Method.GetParameters().Length == 0)
                method.Method.Invoke(null, new object[] { });
            else
            {
                Type paramType = method.Method.GetParameters()[0].ParameterType;
                method.Method.Invoke(null, new object[] { SpecialConvert.Convert(paramType, reference, args) });

            }               
        }
    }
}