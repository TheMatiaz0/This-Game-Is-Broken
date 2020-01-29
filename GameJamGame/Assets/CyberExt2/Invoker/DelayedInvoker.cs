using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

namespace Cyberevolver.Unity
{
    public class DelayedInvoker:MonoBehaviourPlus
    {
        [SerializeField] private SerializeTimeSpan delay = new SerializeTimeSpan();
        [SerializeField] private UnityEvent onTimeOver = null;
        private AsyncStoper stoper;
        [SerializeField] private bool activeOnStart=false,autoReset = false;
        protected override void Awake()
        {
            stoper = new AsyncStoper(this, delay.TimeSpan)
            {
                AutoReset = autoReset
            };
            stoper.OnEnd += (s, e) => onTimeOver.Invoke();
            if (activeOnStart)
                stoper.Start();
          
        }
        public void Run()
        {
            stoper.Start();
        }
    }
}
