using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Cyberevolver.Unity
{
    public class BetterInputField : InputField
    {
        [Tooltip("Focus (update)")]
        [SerializeField] private UnityEvent isFocusedUpdate = new UnityEvent();
        [Tooltip("If user is not focusing input field... (update)")]
        [SerializeField] private UnityEvent isNotFocusedUpdate = new UnityEvent();
        [Tooltip("Focus (onlyOnce)")]
        [SerializeField] private UnityEvent hasOnceFocus = new UnityEvent();
        [SerializeField] private bool onlyOnce;
        [SerializeField] private bool checkForInput = false;
        [SerializeField] private string searchString = null;
        [SerializeField] private UnityEvent textIsCorrect = new UnityEvent();
        public bool OnlyOnce => onlyOnce;
        public bool CheckForInput => checkForInput;
        public string SearchString => searchString;
        protected void Update()
        {
            IsFocused(this.isFocused);

            if (checkForInput)
            {
                if (string.IsNullOrWhiteSpace(searchString) == false)
                {
                    CheckString();
                }
            }
        }
        private void CheckString()
        {
            if (this.text == searchString)
            {
                textIsCorrect?.Invoke();
            }
        }
        private void IsFocused(bool isTrue)
        {
            if (isTrue == true)
            {
                isFocusedUpdate.Invoke();

                OnceFocus();
            }

            else
            {
                isNotFocusedUpdate.Invoke();
            }
        }
        private void OnceFocus()
        {
            if (onlyOnce == false)
            {
                hasOnceFocus.Invoke();
                onlyOnce = true;
            }
        }
    }
}

