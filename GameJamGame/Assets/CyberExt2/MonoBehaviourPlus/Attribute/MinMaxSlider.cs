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
    [CyberAttributeUsage(LegalTypeFlags.Vector2|LegalTypeFlags.Vector2Int)]
    [AttributeUsage(AttributeTargets.Field)]
    public class MinMaxSliderAttribute : CyberAttrribute
    {
      

        public float Min { get; }
        public float Max { get; }
        public MinMaxSliderAttribute(float min, float max)
        {
            Min = min;
            Max = max;
        }
        public MinMaxSliderAttribute(int min, int max)
            : this((float)min, max) { }
    }
}
