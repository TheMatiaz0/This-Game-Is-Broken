using Cyberevolver;
using Cyberevolver.Unity;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CanvasMenu : MonoBehaviourPlus
{

    [Serializable]
    [ShowCyberInspector]
    protected struct ButtonWithEvent
    {
        [field: SerializeField]
        public Text Button { get; private set; }
        [field: SerializeField]
        public UnityEvent OnClick { get; private set; }
    }

    [SerializeField]
    protected ButtonWithEvent[] buttons = null;
    [SerializeField]
    private Color selectColor = Color.yellow;
    [SerializeField]
    private Color nonSelectColor = Color.white;

    private InputActions inputActions;
    private Vector2 movement;

    private Cint selectId = 0;

    protected new void Awake()
    {
        base.Awake();
        inputActions = new InputActions();
        inputActions.PauseControls.UpDown.performed += ctx => movement = ctx.ReadValue<Vector2>();
    }

    protected void OnEnable()
    {
        inputActions.Enable();
    }
    protected void OnDisable()
    {
        inputActions.Disable();
    }


    protected virtual void Start()
    {
        foreach (ButtonWithEvent item in buttons)
        {
            Button trueButton = item.Button.GetComponentInChildren<Button>();
            if (trueButton == null)
                continue;
            trueButton.onClick.AddListener(() => item.OnClick.Invoke());
            EventTrigger trigger = item.Button.gameObject.TryGetElseAdd<EventTrigger>();
            trigger.Add(EventTriggerType.PointerEnter, (b) => selectId = (uint)buttons.GetIndex(element => element.Button == item.Button));

        }
    }

    protected virtual void Update()
    {
        // Debug.Log($"{movement.x}, {movement.y}");
        if (buttons.Length == 0)
            return;

        if (movement.y > 0)
        {
            selectId--;
        }

        else if (movement.y < 0)
        {
            selectId++;
        }

        if (selectId >= buttons.Length)
            selectId = (Cint)(uint)(buttons.Length - 1);

        for (int x = 0; x < buttons.Length; x++)
            buttons[x].Button.color = (selectId == x) ? selectColor : nonSelectColor;

        if (inputActions.PauseControls.Click.triggered)
            buttons[selectId].OnClick.Invoke();

    }

    public void Quit()
    {
        SceneManager.LoadScene("Main Menu");
    }
}