using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cyberevolver;
using Cyberevolver.Unity;
using UnityEngine;
using System.Collections;

namespace Cyberevolver.EditorUnity
{
   public class DrawerAttribute:Attribute
    {
        public Type Target { get; }
        public DrawerAttribute(Type target)
        {
            Target = target;
        }
    }
}

