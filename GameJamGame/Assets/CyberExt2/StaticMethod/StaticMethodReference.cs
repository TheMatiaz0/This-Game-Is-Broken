using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
namespace Cyberevolver.Unity
{
    [Serializable]
    public class StaticMethodReference
    {
        [SerializeField]
        private string _TypeName = null;
        [SerializeField]
        private string _MethodName = null;
        public Type MethodMainType => Type.GetType(_TypeName);
        public MethodInfo Method => Type.GetType(_TypeName).GetMethod(_MethodName);     
    }

}
