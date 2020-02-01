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
        Jumping = 3
    }
    private bool isWalkSound = false;
    public KeyCode JumpKey { get; set; } = KeyCode.Space;
    public KeyCode LeftKey { get; set; } = KeyCode.LeftArrow;
    public KeyCode RightKey { get; set; } = KeyCode.RightArrow;
    [Auto]
    public SpriteRenderer Sprite { get; private set; }

    public void ClearRandomEffect()
    {
        if (currentGlitches.Count!=0)
        {
            var index = UnityEngine.Random.Range(0, currentGlitches.Count);
            currentGlitches[index].Cancel();
            currentGlitches.RemoveAt(index);
          

        }
    }

    private static readonly string AnimatorValueName = "Pose";
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


    public event EventHandler OnPlayerDeath = delegate { };
    private float timeOnStart;



    [Auto]
    public AudioSource Source { get; private set; }

    [SerializeField]
    private AudioClip jumpSound;

    [SerializeField]
    private AudioClip gameOverSound;

    [SerializeField]
    private AudioClip walkSound;

    [SerializeField]
    private AudioClip glitchSound;

    [SerializeField]
    private AudioClip repairSound;

    [SerializeField]
    private AudioSource musicSource;

    [SerializeField]
    private Transform minimalYSurvival;

    [SerializeField]
    private Transform maximumYSurvival;


    private float move;
    
    [SerializeField]

    private GameObject deathParticle;
    private bool canJump;

    public float SceneTime => Time.time - timeOnStart;
    private void Start()
    {
        timeOnStart = Time.time;
        if (gameOverManager != null)
            gameOverManager.EnableMenuWithPause(false);
        transform.position = StartRespPoint.position;
    }

    private void Update()
    {
        if (transform.position.y <= minimalYSurvival.transform.position.y || transform.position.y >= maximumYSurvival.transform.position.y)
        {
            Death();
            return;
        }

        if (IsDeath)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        foreach (var item in currentGlitches)
        {
            item.Update();
        }

        if (Input.GetKey(LeftKey))
        {
            move = -1;
        }
        else if (Input.GetKey(RightKey))
        {
            move = 1;
        }

        else
        {
            move = 0;
        }

        move *= MovementSpeed;
        this.Sprite.flipX = (move < 0);



        if (this.Rgb.velocity.y < -1.3f)
            Animator.SetInteger(AnimatorValueName, (int)AnimState.Faling);

        else if (move == 0)
            Animator.SetInteger(AnimatorValueName, (int)AnimState.Idle);
        else
            Animator.SetInteger(AnimatorValueName, (int)AnimState.Walking);


    }
    public void PushBugs(GlitchEffect effect)
    {
        Source.PlayOneShot(glitchSound);
        currentGlitches.Add(effect);
        global::Console.Instance.GetWriter().WriteLine(effect.Description);
        effect.WhenCollect();
    }

    public void HammerUsage()
    {
        Source.PlayOneShot(repairSound);
        ClearRandomEffect();

    }

    public void Death()
    {
        if (IsDeath == false)
        {
            UIHider hider = Camera.main.GetComponent<UIHider>();
            hider.HideUI(true);
            musicSource.Stop();
            Source.PlayOneShot(gameOverSound);
            StartCoroutine(DeathProcess());
        }

    }
    private IEnumerator DeathProcess()
    {
        IsDeath = true;
        OnPlayerDeath.Invoke(this, EventArgs.Empty);
        Destroy(this.Rgb);
        if (deathParticle != null)
        {
            Instantiate(deathParticle, this.transform.position, Quaternion.identity);
        }
        this.Sprite.enabled = false;
        yield return Async.Wait(TimeSpan.FromSeconds(3));
        GameObject go = GameObject.FindGameObjectWithTag("GameOverObject");
        go.transform.GetChild(0).gameObject.SetActive(true);

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Ground")
        {
            canJump = true;
        }
    }



    private void FixedUpdate()
    {
        if (IsDeath == true)
        {
            return;
        }

        Move();

        if (Input.GetKeyDown(JumpKey))
        {
            Jump();

        }
    }

    private void Jump()
    {
        if (canJump==false)
        {
            return;
        }
        Source.PlayOneShot(jumpSound);
        canJump = false;
        Rgb.velocity = new Vector2(Rgb.velocity.x, Vector3.up.y * JumpMultiple);
    }

    private void Move()
    {
        Rgb.velocity = new Vector2(move * Vector2.right.x, Rgb.velocity.y);
        /*
        isWalkSound = true;
        base.Invoke(
            () =>
            {
                isWalkSound = false;
                Source.PlayOneShot(walkSound);
                //
            }, 1);
            */
        
    }
}
