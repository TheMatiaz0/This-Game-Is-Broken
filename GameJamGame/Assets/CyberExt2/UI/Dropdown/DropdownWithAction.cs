using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Cyberevolver.Unity
{
    
    #pragma warning disable IDE0044
    public class DropdownWithAction : MonoBehaviourPlus
    {
        [Auto]
        private Dropdown Dropdown { get; set; }

        [Tooltip("Set events to number of Options in Dropdown")]
        [SerializeField] private UnityEvent[] unityEvents = null;
        protected override void Awake()
        {
            base.Awake();
            Dropdown.onValueChanged.AddListener(OnDropdownValueChanged);
        }
        public void OnDropdownValueChanged(int choice)
        {
            unityEvents[choice].Invoke();
        }
    }
}
