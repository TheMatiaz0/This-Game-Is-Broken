using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
#pragma warning disable IDE0044

namespace Cyberevolver.Unity
{
    public class OnDoubleClick : CanvasBehaviour
    {
        [SerializeField] private UnityEvent onDoubleClick = null;
        [SerializeField] [Range(0f, 1f)] private float secondClickDelay = 0.1f;
        private AsyncStoper stoper = null;
        private bool firstClick = false;
        protected new void Awake()
        {
            base.Awake();
            stoper = new AsyncStoper(this, TimeSpan.FromSeconds(secondClickDelay), (s, e) => firstClick = false);
        }
        protected override void PointerGuiClick(BaseEventData data)
        {
            if (Input.GetMouseButtonUp(0) == false)
                return;
            if (firstClick == true)
                onDoubleClick.Invoke();
            else
            {
                stoper.Start();
                firstClick = true;
            }
        }


    }
}
