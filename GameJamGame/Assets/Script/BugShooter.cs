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

    [SerializeField]
    private AudioClip  shootSound = null;
    [SerializeField, RequiresAny]
    private Collider2D headCollider   = null;
    [SerializeField, Range(0.1f, 45)]
    private float      seeLenght      = 3f;
    [SerializeField, Range(0, 1000)]
    private int        bounceForce    = 350;
    [SerializeField, Min(0)]
    private int        scoreReward    = 10;
    [SerializeField, Range(0.01f, 10)] 
    private float      bulletSpeed    = 1;
    [SerializeField, Range(0.1f, 10)]
    private float      shootSpeed     = 1;
    [SerializeField]
    private Bullet     bulletPrefab   = null;
    [SerializeField]
    private Transform  shootPoint     = null;

    public override bool IsBad => true;

    [Auto]
    public  SpriteRenderer     Renderer { get; private set; }
    [Auto]
    public  Animator           Animator { get; private set; }



    public void Shoot(Direction dir)
    {

        FastAudio.PlayAtPoint(this.transform.position, shootSound);
        var bullet= Instantiate(bulletPrefab, this.shootPoint.transform.position, Quaternion.identity);
        bullet.Dir = dir;
        bullet.Speed = bulletSpeed;
    }
    protected override void Start()
    {
        base.Start();
       
        StartCoroutine(Shooting());
    }
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
       

        if (headCollider.IsTouching(PlayerController.Instance.Foot) &&
            collision.GetComponent<PlayerController>() &&
            PlayerController.Instance.Rgb.velocity.y < 1)
        {

            WhenPlayerJumped();
        }
        else
        {
            base.OnTriggerEnter2D(collision);
        }

    }
    protected override void OnColidWithPlayer(PlayerController player)
    {
        PlayerController.Instance.PushBugs(GlitchEffect.GetRandomGlitchEffect());
        Explode();
       
    }

    private IEnumerator Shooting()
    {
        while(true)
        {
            Vector2 difference = (Vector2)(PlayerController.Instance.transform.position - this.transform.position);
            if (difference.magnitude <= seeLenght&&difference.magnitude>=3.5f)
            {
                Animator.SetTrigger("shoot");
                Shoot((Direction)difference);
                yield return Async.Wait(TimeSpan.FromSeconds(1f / shootSpeed));

            }
            else
                yield return Async.NextFrame;
           
         
           
        }
    }
 
    protected override void OnExplode()
    {
        Animator.SetTrigger("isDead");
        StopAllCoroutines();
        Destroy(this.GetComponent<Collider2D>());
        Invoke(() => DestroyWithEffect(), 0.55f);

    }
   
    public void WhenPlayerJumped()
    {
        PlayerController.Instance.PlayJumpSound();
        
        Explode();
        PlayerController.Instance.Rgb.AddForce(Vector2.up * bounceForce);
        GameManager.Instance.AddScore(scoreReward);
    }

    

}