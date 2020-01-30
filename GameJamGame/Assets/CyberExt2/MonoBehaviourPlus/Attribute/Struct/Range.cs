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

   
   
    [Serializable]
   
    public struct Range  
    {
       internal const int PrefixSize = 13;
        public  float RandomRange()
        {
            return UnityEngine.Random.Range(this.Min, this.Max);
        }
        public  float GetCenterPoint()
           
        {
            return (this.Max - this.Min / 2f);
        }

        [SerializeField]
        private Vector2 val;
        public float Min
        {
            get => val.x;
            set => val.x = value;
        }

        public float Max
        {
            get => val.y;
            set => val.x = value;
        }
        public Range(float min, float max)
        {
            if (min > max)
                throw new ArgumentException("Min cannot be lower than max", nameof(min));
            val = new Vector2();
            val.x = min;
            val.y = max;
        }
        

    }
   

}
