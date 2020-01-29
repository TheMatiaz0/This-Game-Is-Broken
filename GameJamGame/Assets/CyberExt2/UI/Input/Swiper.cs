using UnityEngine;

namespace Cyberevolver.Unity
{
   
    public class Swiper : MonoBehaviour
    {        
        private Vector2 FirstPressedPosition { get; set; }      
        public Vector2 CurrentSwipe { get; private set; }
        [SerializeField] private MouseButton defaultMouseButton = MouseButton.Left;
        [SerializeField] private Transform objectToSwipe = null;
        protected void Update()
        {
            Swipe();
        } 
        private void Swipe()
        {
            if (Input.GetMouseButtonDown((int)defaultMouseButton))
            {
                FirstPressedPosition = (objectToSwipe != null) ? objectToSwipe.position : Input.mousePosition;
            }
            if (Input.GetMouseButtonUp((int)defaultMouseButton))
            {
                CurrentSwipe = ((Vector2)Input.mousePosition - FirstPressedPosition).normalized;
            }
        }
    }
}
