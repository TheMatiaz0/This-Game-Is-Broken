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
    public class RequireToolbarAttribute : CyberAttrribute
    {
        //I cannot use null value here :(

        public bool HasOnlyStringValue { get; }
        public int Index { get; }
        public string IndexAsName { get; }
        public string ToolbarId { get; }
        public RequireToolbarAttribute(string toolbarId, int index)
        {
           
            ToolbarId = toolbarId ?? throw new ArgumentNullException(nameof(toolbarId));
            Index = index;
        }
        public RequireToolbarAttribute(string toolbardId, string element)
            :this(toolbardId,0)
        {
            IndexAsName = element;
            HasOnlyStringValue = true;
        }

    }
}
