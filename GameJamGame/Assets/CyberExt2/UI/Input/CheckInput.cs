using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Cyberevolver.Unity
{
#pragma warning disable IDE0044
    public class CheckInput : MonoBehaviourPlus
    {
        [SerializeField] private string searchString = null;
        [SerializeField] private Text searchText = null;

        [SerializeField] private UnityEvent textIsCorrect = new UnityEvent();
        [Auto]
        public InputField InputField { get; private set; }

        protected void Update()
        {
            if (searchText != null)
            {
                CheckText();
            }

            if (string.IsNullOrWhiteSpace(searchString) == false)
            {
                CheckString();
            }

        }

        private void CheckText()
        {
            if (InputField.text == searchText.text)
            {
                textIsCorrect?.Invoke();
            }
        }

        private void CheckString()
        {
            if (InputField.text == searchString)
            {
                textIsCorrect?.Invoke();
            }
        }
    }
}

