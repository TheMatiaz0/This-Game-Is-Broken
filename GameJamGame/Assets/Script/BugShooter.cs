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

public class BugShooter : ActiveElement
{
    public override bool IsBad => true;
    [SerializeField]
    private Collider2D headCollider;
    [SerializeField]
    [Range(0.1f, 45)]
    private float seeLenght = 3f;
    [SerializeField]
    private int BounceForce = 350;
    [SerializeField]
    [Range(0.01f, 10)]
    private float bulletSpeed = 1;
    [SerializeField]
    [Range(0.1f, 10)]
    private float shootSpeed = 1;
    [SerializeField]
    private Bullet bulletPrefab = null;
    [SerializeField]
    private Transform shootPoint;
    [Auto]
    public SpriteRenderer Renderer { get; private set; }
    [Auto]
    public Animator Animator { get; private set; }

    private Collider2D heroCollider;

    public void Shoot(Direction dir)
    {
        var bullet= Instantiate(bulletPrefab, this.shootPoint.transform.position, Quaternion.identity);
        bullet.Dir = dir;
        bullet.Speed = bulletSpeed;
    }
    protected override void Start()
    {
        base.Start();
        heroCollider= PlayerController.Instance.GetComponent<Collider2D>();
        StartCoroutine(Shooting());
    }
    private IEnumerator Shooting()
    {
        while(true)
        {
            yield return Async.Wait(TimeSpan.FromSeconds(1f/shootSpeed));
         
            Vector2 difference = (Vector2)(PlayerController.Instance.transform.position - this.transform.position);
            if(difference.magnitude<=seeLenght)
            {
                Animator.SetTrigger("shoot");
                Shoot((Direction)difference);
                
            }
        }
    }
  
    protected override void OnColidWithPlayer(PlayerController player)
    {
        PlayerController.Instance.Kill();
    }

    public override void OnExplode()
    {
        Animator.SetTrigger("isDead");
  
    }
    bool end = false;
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (end)
            return;

        if (headCollider.IsTouching(heroCollider)&&collision.GetComponent<PlayerController>())
        {

            end = true;
            StopAllCoroutines();
            OnExplode();
            Destroy(this.GetComponent<Collider2D>());
            Invoke(() => DestroyWithEffect(), 0.55f);
            PlayerController.Instance.Rgb.AddForce(Vector2.up * BounceForce);
        }
        else
        {
            base.OnTriggerEnter2D(collision);
        }
      
        
    }
}