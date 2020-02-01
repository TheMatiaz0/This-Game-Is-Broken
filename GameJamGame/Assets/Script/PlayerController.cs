using Cyberevolver.Unity;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public sealed class PlayerController : AutoInstanceBehaviour<PlayerController>
{
    public bool IsDeath { get; private set; }
    public enum AnimState
    {
        Idle = 0,
        Walking = 1,
        Faling = 2,
    }
    [Auto]
    public SpriteRenderer Sprite { get; private set; }
    private static readonly string AnimtorValueName = "Pose";
    [Auto]
    public Animator Animator { get; private set; }
    [Auto]
    public Rigidbody2D Rgb { get; set; }
    private readonly List<GlitchEffect> currentGlitches = new List<GlitchEffect>();

    [field: SerializeField, Cyberevolver.Unity.MinMaxRange(0f, 20f)]
    public float MovementSpeed { get; private set; } = 0.11f;

    [field: SerializeField, Cyberevolver.Unity.MinMaxRange(0f, 400f)]
    public float JumpMultiple { get; private set; }

    [SerializeField]
    private FreezeMenu gameOverManager = null;

    [SerializeField]
    private Transform startRespPoint;

    public Transform StartRespPoint => startRespPoint;



    private float move;
    private bool hasJumped = false;

    void Start()
    {
        if (gameOverManager != null)
            gameOverManager.EnableMenuWithPause(false);
        transform.position = StartRespPoint.position;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }


        this.Sprite.flipX = (move < 0);
        move = Input.GetAxisRaw("Horizontal") * MovementSpeed;
        if (this.Rgb.velocity.y < 0)
            Animator.SetInteger(AnimtorValueName, (int)AnimState.Faling);
        else if (move != 0)
            Animator.SetInteger(AnimtorValueName, (int)AnimState.Walking);
        else
            Animator.SetInteger(AnimtorValueName, (int)AnimState.Idle);


    }

    public void Death ()
    {
        if(IsDeath==false)
        {
            IsDeath = true;
         
           
            
        }

    }
    private IEnumerator DeathProcess()
    {
        this.Rgb.Sleep();
        yield return Async.Wait(TimeSpan.FromSeconds(1));
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground")
        {
            hasJumped = false;
        }
    }

    private void FixedUpdate()
    {
        Move();

        if (Input.GetButtonDown("Jump"))
        {
            Jump();
        }
    }

    private void Jump()
    {
        if (hasJumped == true)
        {
            return;
        }
        Rgb.velocity = new Vector2(Rgb.velocity.x, Vector3.up.y * JumpMultiple);
        hasJumped = true;
    }

    private void Move()
    {
        Rgb.velocity = new Vector2(move * Vector2.right.x, Rgb.velocity.y);
    }
}
