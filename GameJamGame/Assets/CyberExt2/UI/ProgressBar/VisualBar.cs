
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
namespace Cyberevolver.Unity
{
#pragma warning disable IDE0044
    public class VisualBar : MonoBehaviour,ISerializationCallbackReceiver
    {
        private const string ReferName = "Reference";
        private const string EventName = "Events";
        private const string ValuesName = "Values";     
        [Serializable] public class PercentEvent : UnityEvent<Percent> { }
        [Serializable] public class EventPair : Pair<Percent, UnityEvent> { }
        //Serializable private
        [SerializeField] private Image progressBar = null;
        [SerializeField] private Slider progressBarAsSlider = null;
        [SerializeField] private Text textIndicator = null;
		[SerializeField] private bool textIndicatorActive = true;
        [SerializeField]
        [Range(1f, 400f)]
        private float _speed = 30f;
        private Percent _CurrentAmount = 0;
        private Percent _PreferredAmount = 0;
        [SerializeField] private PercentEvent onPreferredChanged = null;
          [SerializeField] private PercentEvent onCurrentChanged = null;
          [SerializeField] private EventPair[] onValueIsEvents = null;
        //private
        private Percent before;
        private Dictionary<object, Dictionary<Percent, List<Action<VisualBar, Percent>>>> registeredAction = new Dictionary<object, Dictionary<Percent, List<Action<VisualBar, Percent>>>>();
        private bool isChanging = true;

		public float Speed
		{
			get => _speed;

			set
			{
				if (value != _speed)
				{
					_speed = value;
				}
			}
		}

        //properties
        public Percent CurrentAmount
        {
            get => _CurrentAmount;
            set
            {
                if (value != _CurrentAmount)
                {
                    _CurrentAmount = value;
                    OnCurrentChanged(this, new VisualBarArgs(this, PreferredAmount, CurrentAmount));
                }

            }
        }
        public Percent PreferredAmount
        {
            get => _PreferredAmount;
            set
            {
                if (value != _PreferredAmount)
                {
                    OnPreferredChanged(this, new VisualBarArgs(this, PreferredAmount, CurrentAmount));
                    _PreferredAmount = value;
                }

            }
        }
        public Image ProgessBar => progressBar;
        public Slider ProgressBarAsSlider => progressBarAsSlider;
        public Text TextIndicator => textIndicator;
        //events

        public event EventHandler<VisualBarArgs> OnPreferredChanged = delegate { };
        public event EventHandler<VisualBarArgs> OnCurrentChanged = delegate { };
        public void RegisterWaiterFor(object register, Percent valueWhichYouWait, Action<VisualBar, Percent> action)
        {
            if (registeredAction.ContainsKey(register) == false)
                registeredAction.Add(register, new Dictionary<Percent, List<Action<VisualBar, Percent>>>());
            if (registeredAction[register].ContainsKey(valueWhichYouWait) == false)
                registeredAction[register].Add(valueWhichYouWait, new List<Action<VisualBar, Percent>>());
            registeredAction[register][valueWhichYouWait].Add(action);

        }
        public void UnregisterWaiterFor(object register)
        {
            registeredAction.Remove(register);
        }
        private void CheckEvent()
        {
            if (onValueIsEvents != null)
            {
                foreach (Pair<Percent, UnityEvent> pair in onValueIsEvents)
                {
                    if (InRange(pair.First))
                        pair.Second.Invoke();
                }
                foreach (KeyValuePair<Percent, List<Action<VisualBar, Percent>>> act in registeredAction.SelectMany(item => item.Value))
                {

                    if(InRange(act.Key))
                        foreach (Action<VisualBar, Percent> action in act.Value)
                            action.Invoke(this, CurrentAmount);
                }
            }

        }
        private bool InRange(Percent val)
        {
            return val <= CurrentAmount && val > before
                      || (val >= CurrentAmount && val < before);
        }
        protected virtual void Start()
        {
            OnPreferredChanged += (s, e) => onPreferredChanged.Invoke(e.Preferred);
            OnPreferredChanged += (s, e) => isChanging = true;
            OnCurrentChanged += (s, e) => onCurrentChanged.Invoke(e.Showing);
        }
        protected virtual void Update()
        {

            if (isChanging)
            {
                before = CurrentAmount;
                if (CurrentAmount != PreferredAmount)
                {
                    if (CurrentAmount < PreferredAmount.AsDoubleValue)
                    {
                        Increase();
                    }
                    else
                    {
                        Decrease();
                    }
                }

                CheckEvent();
                Apply();
            }         
            if (InRange(PreferredAmount))
            {
                CurrentAmount = PreferredAmount;
                isChanging = false;
                Apply();
            }
               
        }
        private void Apply()
        {
            if (textIndicator != null && textIndicatorActive == true)
            {
                textIndicator.text = $"{(CurrentAmount).ToString()}";
            }
            if (progressBar != null)
            {
                progressBar.fillAmount = (float)(CurrentAmount);
            }

            else if (progressBarAsSlider != null)
            {
                progressBarAsSlider.value = (float)(CurrentAmount);
            }
        }

        private void Increase()
        {
            CurrentAmount += (double)(Speed / 100.0 * Time.deltaTime);

        }

        private void Decrease()
        {
            CurrentAmount -= (double)(Speed / 100.0 * Time.deltaTime);

        }
       
        

        void ISerializationCallbackReceiver.OnBeforeSerialize()
        {
           
        }

        void ISerializationCallbackReceiver.OnAfterDeserialize()
        {
            OnPreferredChanged(this, new VisualBarArgs(this, PreferredAmount, CurrentAmount));
            OnCurrentChanged(this, new VisualBarArgs(this, PreferredAmount, CurrentAmount));
        }
    }

}
