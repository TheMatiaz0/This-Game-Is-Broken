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

public abstract class ActiveElement : MonoBehaviourPlus
{

    protected const string EventFold = "Events";

    [SerializeField]
    [AssetOnly]
    private GameObject onKillPrefab;
    [SerializeField,Foldout(EventFold)]
    private UnityEvent onKilled;
    protected virtual void OnColidWithPlayer(PlayerMovement player) { }
    protected virtual void OnKill() { }
    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerMovement player;
        if((player= collision.GetComponent<PlayerMovement>())!=null)
        {
            OnColidWithPlayer(player);
           
           
        }
    }
    public void DestroyWithEffect()
    {
        OnKill();
        Instantiate(onKillPrefab).transform.position = this.transform.position;
        onKilled.Invoke();
        Destroy(this.gameObject);
        
    }
}