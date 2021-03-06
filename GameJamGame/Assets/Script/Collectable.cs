﻿using System;
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

public abstract class Collectable : ActiveElement
{


    [SerializeField]
    private int        score           = 0;
    [SerializeField]
    private GameObject onCollectEffect = null;
    [SerializeField, Foldout(EventFold)]
    private UnityEvent onCollect       = null;


    private bool wasCollect = false;
    protected sealed override void OnColidWithPlayer(PlayerController player)
    {
        if(wasCollect)
        {
            return;
        }
        wasCollect = true;
        GameManager.Instance.AddScore(score);
        onCollect.Invoke();
        OnCollect();
        if (onCollectEffect != null)
            Instantiate(onCollectEffect, this.transform.position, Quaternion.identity);
        DestroyWithEffect();
       
    }
    protected virtual void OnCollect() { }
}