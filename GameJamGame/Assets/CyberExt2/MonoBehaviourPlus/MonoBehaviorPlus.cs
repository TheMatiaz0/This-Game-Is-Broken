using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using System.Reflection;
using System.Collections;

namespace Cyberevolver.Unity
{
    public class MonoBehaviourPlus : MonoBehaviour
    {
    
        private static readonly BindingFlags flags = BindingFlags.Public | BindingFlags.Instance | BindingFlags.NonPublic;
        private IEnumerable<PropertyInfo> GetAllRequirer() => this.GetType().GetProperties(flags).Where(item => item.GetCustomAttribute<AutoAttribute>() != null);
        protected virtual void Awake()
        {
            foreach (PropertyInfo item in GetAllRequirer())
                item.SetValue(this, this.gameObject.TryGetElseAdd(item.PropertyType));
           
             
        }
       
        public RaycastHit2D Ray2DWithoutThis(Vector2 from,Direction dir, float distance)
        {
            return Physics2D.RaycastAll(from, dir, distance)
                .FirstOrDefault(item => item.collider != null && item.collider.gameObject != this.gameObject);
        }
        
        #region Invoke
        protected MethodDelay InvokeRepeating(Action action, float seconds)
        {
            MethodDelay delay = new MethodDelay(isRepeating: true);
            StartCoroutine(PInvoke(action, seconds, delay));
            return delay;
        }
        protected MethodDelay InvokeRepeating(Action action, float seconds, int limit)
        {
            MethodDelay delay = new MethodDelay(true, limit);
            StartCoroutine(PInvoke(action, seconds, delay));
            return delay;
        }
        protected MethodDelay Invoke(Action action, float seconds)
        {
            MethodDelay delay = new MethodDelay();
            StartCoroutine(PInvoke(action, seconds, delay));
            return delay;
        }
        private IEnumerator PInvoke(Action action, float seconds, MethodDelay delay)
        {
            do
            {
                yield return new WaitForSeconds(seconds);
                if (delay.HasStopped == false)
                {
                    action.Invoke();

                }

            } while (delay.IsRepeating && ((delay.LimitedRepating == true && --delay.RepatingValue > 0) || delay.LimitedRepating == false) && delay.HasStopped == false);
            delay.InvokeOnEnd();
        }
        #endregion
        #region Deactived
        [Obsolete("", true)]
        protected new void Invoke(string methodName, float time) { }
        [Obsolete("", true)]
        protected new void InvokeRepeating(string methodName, float time, float repateRat) { }
      
        #endregion

    }

}
