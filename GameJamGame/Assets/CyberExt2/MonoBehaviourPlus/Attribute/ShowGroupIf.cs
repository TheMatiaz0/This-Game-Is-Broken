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
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
    public class ShowGroupIfAttribute : CondtionsAttribute
    {
        public string Folder { get;  }
        public ShowGroupIfAttribute(string folder,string serializedProp, object value) : base(serializedProp, value)
        {
            Folder = folder;
        }

        public ShowGroupIfAttribute(string folder,string serializedProp, Equaler equaler, object value) : base(serializedProp, equaler, value)
        {
            Folder = folder;
        }

        public ShowGroupIfAttribute(string folder,string serializedProp) : base(serializedProp)
        {
            Folder = folder;
        }
    }
}
