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
    [AttributeUsage(AttributeTargets.Field|AttributeTargets.Method)]
    public class IndentLvAttribute : CyberAttrribute
    {
        public int Quantity { get; }
        public bool OneShoot { get; set; }
        public IndentLvAttribute(int quantity)
        {
            Quantity = quantity;
        }
    }
}

