using UnityEngine;
using UnityEngine.UI;


namespace Cyberevolver.Unity
{
   
    public class BetterImage : Image
    {
        [SerializeField]
        public new bool preserveAspect;

        protected override void Awake()
        {
            base.Awake();
            base.preserveAspect = preserveAspect;
        }


    }

}
