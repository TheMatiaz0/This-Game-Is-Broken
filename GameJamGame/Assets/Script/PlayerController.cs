using Cyberevolver.Unity;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public sealed class PlayerController : ActiveElement
{
    public bool IsDeath { get; private set; }
    public enum AnimState
    {
        Idle = 0,
        Walking = 1,
        Faling = 2,
        Jumping = 3
    }
    public KeyCode JumpKey { get; set; } = KeyCode.Space;
    public KeyCode LeftKey { get; set; } = KeyCode.LeftArrow;
    public KeyCode RightKey { get; set; } = KeyCode.RightArrow;
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

    public static PlayerController Instance { get; private set; }

    protected override void Awake()
    {
        Instance = this;
    }

    private float move;
    private bool hasJumped = false;

    protected override void Start()
    {
        base.Start();
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

        foreach(var item in currentGlitches)
        {
            item.Update();
        }
      
        if(Input.GetKey(LeftKey))
        {
            move = -1;
        }
        else if(Input.GetKey(RightKey))
        {
            move = 1;
        }
        else
        {
            move = 0;
        }
        move *= MovementSpeed;
        this.Sprite.flipX = (move < 0);


        if (this.Rgb.velocity.y < 0)
            Animator.SetInteger(AnimtorValueName, (int)AnimState.Faling);
        else if (move != 0)
            Animator.SetInteger(AnimtorValueName, (int)AnimState.Walking);
        else
            Animator.SetInteger(AnimtorValueName, (int)AnimState.Idle);


    }
    public void PushBugs(GlitchEffect effect)
    {
        currentGlitches.Add(effect);
        global::Console.Instance.UpdateConsole(effect.Description);
        effect.WhenCollect();
    }
    public void Death ()
    {
        if(IsDeath==false)
        {
            IsDeath = true;
            StartCoroutine(DeathProcess());
           
            
        }

    }
    private IEnumerator DeathProcess()
    {
        Destroy( this.Rgb);
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

        if (Input.GetKeyDown(JumpKey))
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
        Animator.SetInteger(AnimtorValueName, (int)AnimState.Jumping);
        Rgb.velocity = new Vector2(Rgb.velocity.x, Vector3.up.y * JumpMultiple);
        hasJumped = true;
    }

    private void Move()
    {
        Rgb.velocity = new Vector2(move * Vector2.right.x, Rgb.velocity.y);
    }
}
