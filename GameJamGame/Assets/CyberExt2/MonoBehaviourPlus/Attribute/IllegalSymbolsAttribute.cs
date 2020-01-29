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
    public class IllegalSymbolsAttribute : CyberAttrribute
    {
        

        public string[] Symbols { get; }
        public IllegalSymbolsAttribute(params string[] symbols)
        {
            Symbols = symbols ?? new string[0];
        }
    }
}
