using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
namespace Cyberevolver.Unity
{
    public abstract class AutoInstanceBehaviour<T> : MonoBehaviourPlus
    where T : AutoInstanceBehaviour<T>
    {
        public static T Instance { get; private set; }
        protected override void Awake()
        {
            base.Awake();
            Instance = (T)this;
        }
    }
}
