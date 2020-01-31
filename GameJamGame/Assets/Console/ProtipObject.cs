using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Cyberevolver.Unity;
using Cyberevolver;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ProtipObject:CanvasBehaviour
{
    [SerializeField] private Text textEntity;
    public Text TextEntity => textEntity;
    public bool IsMouseColliding { get; private set; }
    public void Init(string text)
    {
        textEntity.text = text;
    }
    protected override void PointerGuiAreaEnter(BaseEventData data)
    {
        base.PointerGuiAreaEnter(data);
        IsMouseColliding = true;
    }
    protected override void PointerGuiAreaExit(BaseEventData data)
    {
        base.PointerGuiAreaExit(data);
        IsMouseColliding = false;
    }
    public void Put()
    {

        DeveloperConsole.Instance.Put(textEntity.text);
    }
}

