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
    public class BorderGroupAttribute : GroupAttribute
    {
        public BorderGroupAttribute(string folder) : base(folder)
        {
        }
    }
}
