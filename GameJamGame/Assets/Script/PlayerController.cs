using Cyberevolver;
using Cyberevolver.Unity;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
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
        if (currentGlitches.Count != 0)
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
    public float MovementSpeed { get; set; } = 0.11f;

    [field: SerializeField, Cyberevolver.Unity.MinMaxRange(0f, 400f)]
    public float JumpMultiple { get; private set; }

    [SerializeField]
    private FreezeMenu gameOverManager = null;

    [SerializeField]
    private Transform startRespPoint;

    public Transform StartRespPoint => startRespPoint;


    public event EventHandler OnPlayerDeath = delegate { };
    private float timeOnStart;


    [SerializeField]
    private Camera cam;
    public Camera Cam => cam;

    [SerializeField]
    private Cinemachine.CinemachineVirtualCamera virtualCam;
    public Cinemachine.CinemachineVirtualCamera Virtual => virtualCam;

    public float PrefferedCameraZoom { get; set; }
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

    [SerializeField]
    private Transform deathYPoint;
    private float move;
    [SerializeField]
    private int scoreByBugs=-15;
    [SerializeField]

    private GameObject deathParticle;
    private bool canJump;
    public ReadOnlyCollection<GlitchEffect> CurrentGlithes => new ReadOnlyCollection<GlitchEffect>(currentGlitches);

    public float SceneTime => Time.time - timeOnStart;
    private void Start()
    {

        PrefferedCameraZoom = virtualCam.m_Lens.FieldOfView;
        timeOnStart = Time.time;
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

        if (this.transform.position.y < deathYPoint.position.y)
        {
            this.Kill();
        }
        if (IsDeath)
        {
            return;
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
        GameManager.Instance.AddScore(scoreByBugs);
        if (effect == null)
            return;
        Source.PlayOneShot(glitchSound);
        if (currentGlitches.Any(item => item.GetType() == effect.GetType()) == false)
        {
            currentGlitches.Add(effect);
            global::Console.Instance.GetWriter().WriteLine($"<color=#FF3107>ERROR:: <color=#FFCD00>{effect.Description}</color></color>");
            effect.WhenCollect();
        }
    }

    public void HammerUsage()
    {
        Source.PlayOneShot(repairSound);
        ClearRandomEffect();

    }

    public void Kill()
    {
        if (IsDeath == false)
        {
            UIHider hider = Camera.main.GetComponent<UIHider>();
            hider.HideUI(false);
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

        Vector3Int AsInt(Vector3 v)
            => new Vector3Int((int)v.x, (int)v.y, (int)v.z);



        if ((int)(virtualCam.m_Lens.FieldOfView) != (int)(PrefferedCameraZoom))
            virtualCam.m_Lens.FieldOfView += (PrefferedCameraZoom - virtualCam.m_Lens.FieldOfView) / 3;


        Move();

        if (Input.GetKeyDown(JumpKey) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            Jump();
        }
    }
    
    private void Jump()
    {
        if (canJump == false)
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
