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
    private bool       fakeDestroy  = false;
    [SerializeField]
    private GameObject onKillPrefab = null;
    [SerializeField]
    private AudioClip  destroySound = null;
    [SerializeField, Foldout(EventFold)]
    private UnityEvent onKilled     = null;


    bool particleWasSpawn;
    [Auto]
    public Rigidbody2D Rgb { get; protected set; }
    public bool IsKilled { get; private set; }
    public abstract bool IsBad { get; }
    private bool wasExplode;
    protected virtual void Start()
    {
        Rgb.bodyType = RigidbodyType2D.Kinematic;
    }
    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (wasExplode)
            return;
        PlayerController player;
        if ((player = collision.GetComponent<PlayerController>()) != null)
        {
            OnColidWithPlayer(player);
        }
        if (collision.GetComponent<Wave>())
            DestroyWithEffect();
    }
 
    public void DestroyWithEffect()
    {
        if (IsKilled)
            return;
        IsKilled = true;
        OnKill();
        SpawnDeathParticles();
        onKilled.Invoke();
        if (fakeDestroy == false)
            Destroy(this.gameObject);
        
    }
  
    public void SpawnDeathParticles()
    {
        if (particleWasSpawn)
            return;
        particleWasSpawn = true;
        if (onKillPrefab != null)
            Instantiate(onKillPrefab).transform.position = this.transform.position;
        if (destroySound != null)
            FastAudio.PlayAtPoint(this.transform.position, destroySound);
    }
    protected virtual void OnColidWithPlayer(PlayerController player) { }
    protected virtual void OnKill() { }
    public  void Explode()
    {
        if(wasExplode==false)
        {
            wasExplode = true;
            OnExplode();
        }
      
    }
    protected virtual void OnExplode() { }

}