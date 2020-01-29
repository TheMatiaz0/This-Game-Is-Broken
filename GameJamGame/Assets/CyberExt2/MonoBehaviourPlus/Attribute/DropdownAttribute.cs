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
    public class DropdownAttribute : CyberAttrribute
    {
        public object[] Values { get; }
        public string[] Names { get; }
        public bool ShowAsName { get; }

        public DropdownAttribute( string[] names, object[] values,bool showAsName=true)
        {
            Values = values ?? new object[0];
            Names = names;
            ShowAsName = showAsName;
        }
        public DropdownAttribute( object[] values)
            : this(values.Select(item=>item.ToString()).ToArray(), values,false) { }
        

        
    }
}
