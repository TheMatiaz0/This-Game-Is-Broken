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
    public abstract class CondtionsAttribute : CyberAttrribute
    {
        public string Prop { get; }
        public object Value { get; }
        public Equaler Equaler { get; }
        public CondtionsAttribute(string serializedProp, Equaler equaler, object value)
        {
            Prop = serializedProp;
            Equaler = equaler;
            Value = value;

        }
        public CondtionsAttribute(string serializedProp,object value) : this(serializedProp, Equaler.Equal, value) { }
        public CondtionsAttribute(string serializedProp):this(serializedProp,true) { }
      
        
    }
}
