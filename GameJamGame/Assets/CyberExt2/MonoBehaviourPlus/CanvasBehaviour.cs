using UnityEngine;
using UnityEngine.EventSystems;
namespace Cyberevolver.Unity
{
    public abstract class CanvasBehaviour : MonoBehaviourPlus
    {        
        protected EventTrigger Trigger { get; private set; }
        virtual protected GameObject TriggerObject => this.gameObject;
        public bool IsMouseOnTrigger { get; private set; }
        protected override void Awake()
        {            
            base.Awake();
            Trigger = TriggerObject.TryGetElseAdd<EventTrigger>();
            Trigger.Add(EventTriggerType.PointerClick, PointerGuiClick);
            Trigger.Add(EventTriggerType.PointerDown, PointerGuiClickDown);
            Trigger.Add(EventTriggerType.PointerUp, PointerGuiClickUp);
            Trigger.Add(EventTriggerType.PointerEnter, PointerGuiAreaEnter);
            Trigger.Add(EventTriggerType.PointerExit, PointerGuiAreaExit);

            
        }
        protected virtual void Update()
        {
            if (IsMouseOnTrigger)
                PointerGuiArea();

        }
        protected virtual void PointerGuiArea() { }
        protected virtual void PointerGuiClick(BaseEventData data) { }
        protected virtual void PointerGuiClickDown(BaseEventData data) { }
        protected virtual void PointerGuiClickUp(BaseEventData data) { }
        protected virtual void PointerGuiAreaEnter(BaseEventData data) 
        {
            IsMouseOnTrigger = true;
        }
        protected virtual void PointerGuiAreaExit(BaseEventData data)
        {
            IsMouseOnTrigger = false;
        }
    }

}

