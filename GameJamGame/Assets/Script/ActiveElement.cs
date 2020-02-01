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
    public abstract bool IsBad { get; }
    [SerializeField]
    private bool fakeDestroy;
    protected const string EventFold = "Events";

    [Auto]
    public Rigidbody2D Rgb { get; protected set; }
    public bool IsKilled { get; private set; }

    [SerializeField]
    
  

    private GameObject onKillPrefab;
    [SerializeField]
    [Foldout(EventFold)]
    private UnityEvent onKilled;
    protected virtual void OnColidWithPlayer(PlayerController player) { }
    protected virtual void OnKill() { }
    protected virtual void Start()
    {
        Rgb.bodyType = RigidbodyType2D.Kinematic;
    }
    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerController player;
        if((player= collision.GetComponent<PlayerController>())!=null)
        {
            OnColidWithPlayer(player);
        }
    }
   
    public void DestroyWithEffect()
    {
        if (IsKilled)
            return;
        IsKilled = true;
        OnKill();
        if (onKillPrefab != null)
            Instantiate(onKillPrefab).transform.position = this.transform.position;
        onKilled.Invoke();
        if (fakeDestroy == false)
            Destroy(this.gameObject);
        
    }
}