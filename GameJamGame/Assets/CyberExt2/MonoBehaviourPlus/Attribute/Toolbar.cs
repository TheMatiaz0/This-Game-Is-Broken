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
    [AttributeUsage(AttributeTargets.Field|AttributeTargets.Method|AttributeTargets.Class|AttributeTargets.Struct)]
    public class ToolbarAttribute : CyberAttrribute
    {
        public string[] Names { get; }
        public string ToolbarId { get; }
        public ToolbarAttribute(string toolbarId,params string[] other)
        {
            ToolbarId = toolbarId ?? throw new ArgumentNullException(nameof(toolbarId));
            Names = other?? throw new ArgumentNullException(nameof(other));
        }
    }
}
