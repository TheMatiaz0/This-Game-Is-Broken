using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Cyberevolver.Unity
{
    public static class Async
    {
        public static WaitForUpdate NextFrame { get; } = new WaitForUpdate();
        public static WaitForFixedUpdate FixedUpdate { get; } = new WaitForFixedUpdate();
        public static WaitForEndOfFrame EndOfFrame { get; } = new WaitForEndOfFrame();
        public static WaitForAnimator WaitForAnimator(Animator animator, string animName, int layer = 0)
            => new WaitForAnimator(animator, animName, layer);
        public static WaitForAnim WaitForAnim(Animation anim)
            => new WaitForAnim(anim);
        public static WaitForSeconds Wait(TimeSpan time) => new WaitForSeconds((float)time.TotalSeconds);
        public static WaitForSeconds WaitForSeconds(float seconds) => new WaitForSeconds(seconds);
        public static object WaitForSeconds(float seconds, bool realTime)
            => (realTime) ? (object)WaitForSecondsReal(seconds) : (object)WaitForSeconds(seconds);
        public static WaitForSecondsRealtime WaitForSecondsReal(float seconds) 
            => new WaitForSecondsRealtime(seconds);
        public static WaitUntil Until(Func<bool> predicate)
            => new WaitUntil(predicate);
        public static WaitWhile While(Func<bool> predicate) 
            => new WaitWhile(predicate);
    }
}

