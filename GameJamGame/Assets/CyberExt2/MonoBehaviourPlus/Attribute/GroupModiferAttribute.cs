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
    public class GroupModiferAttribute : CyberAttrribute
    {
   
        public string Folder { get; }
        public GroupModiferAttribute(string folder)
        {
            Folder = folder ?? throw new ArgumentNullException(nameof(folder));
        }

    }
}
