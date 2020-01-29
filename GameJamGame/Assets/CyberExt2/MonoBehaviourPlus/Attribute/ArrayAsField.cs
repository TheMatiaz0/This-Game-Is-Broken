﻿using System;
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
    [CyberAttributeUsage(LegalTypeFlags.Array)]
    [AttributeUsage(AttributeTargets.Field)]
    public class ArrayAsFieldAttribute : CyberAttrribute
    {
        public ArrayAsFieldAttribute(params string[] names)
        {
            Names = names;
        }

        public string[] Names { get; }

    }
}
