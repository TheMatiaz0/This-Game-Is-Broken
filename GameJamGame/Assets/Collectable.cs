using System;
using System.Collections.Generic;
using System.Collections;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Cyberevolver;
using Cyberevolver.Unity;
using UnityEngine.Events;

public class Collectable : ActiveElement
{


    [SerializeField]
    private int score = 0;
    [SerializeField]
    [Foldout(EventFold)]
    private UnityEvent onCollect;

    protected sealed override void OnColidWithPlayer(PlayerMovement player)
    {
        GameManager.Instance.AddScore(score);
        onCollect.Invoke();
        OnCollect();
        DestroyWithEffect();
       
    }
    protected virtual void OnCollect() { }
}