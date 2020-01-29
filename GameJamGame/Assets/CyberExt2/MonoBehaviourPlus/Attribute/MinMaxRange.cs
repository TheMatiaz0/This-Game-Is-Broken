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
    public class MinMaxRangeAttribute : CyberAttrribute
    {
      

        public float Min { get; }
        public float Max { get; }
      
        
        public MinMaxRangeAttribute(float min, float max)
        {
            Max = max;
            Min = min;

        }
        public MinMaxRangeAttribute(int min, int max):
            this((float)min,max){}
        
      
       
    }
}
