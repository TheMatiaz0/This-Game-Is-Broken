using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cyberevolver;
using Cyberevolver.Unity;
using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class CanvasMenu : MonoBehaviourPlus
{

    [Serializable]
    [ShowCyberInspector]
    protected struct ButtonWithEvent
    {
        [field:SerializeField]
        public Text Button { get; private set; }
        [field:SerializeField]
        public UnityEvent OnClick { get; private set; }
    }

    [SerializeField]
    protected ButtonWithEvent[] buttons         = null;
    [SerializeField]
    private Color             selectColor     = Color.yellow;
    [SerializeField]
    private Color             nonSelectColor  = Color.white;

    private Cint  selectId = 0;


    protected virtual void Start()
    {
        foreach(ButtonWithEvent item in buttons)
        {
            Button trueButton = item.Button.GetComponentInChildren<Button>();
            if (trueButton == null)
                continue;
            trueButton.onClick.AddListener(()=> item.OnClick.Invoke());
            EventTrigger trigger = item.Button.gameObject.TryGetElseAdd<EventTrigger>();
            trigger.Add(EventTriggerType.PointerEnter, (b) => selectId = (uint)buttons.GetIndex(element => element.Button == item.Button));

        }
    }

    protected virtual void Update()
    {
        if (buttons.Length == 0)
            return;
        if ((Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W)))
            selectId--;//this is cint, so increment is always  safe
        else if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
            selectId++;
        if (selectId >= buttons.Length)
            selectId = (Cint)(uint) (buttons.Length - 1);//It's safer option. If inspektor is changing in play mode it still works.

        for(int x=0;x<buttons.Length;x++)
            buttons[x].Button.color = (selectId == x) ? selectColor : nonSelectColor;

        if(Input.GetKeyDown(KeyCode.Return)||Input.GetKeyDown(KeyCode.Space))
            buttons[selectId].OnClick.Invoke();           

    }

    public void Quit()
    {
        SceneManager.LoadScene("Main Menu");
    }
}