using Cyberevolver;
using Cyberevolver.Unity;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.VFX;
using static UnityEngine.InputSystem.InputAction;

[CustomBackgrounGroup(Asset, BackgroundMode.GroupBox)]

public sealed class PlayerController : AutoInstanceBehaviour<PlayerController>
{

    #region CONST
    private const string Reference = "Reference";
    private const string Asset = "Asset";
    private const string Value = "Value";
    private const string AnimatorValueName = "Pose";
    #endregion
    private enum AnimState
    {
        Idle = 0,
        Walking = 1,
        Faling = 2,
        Jumping = 3
    }


    [SerializeField, BoxGroup(Value)]
    private int scoreByBugs = -15;
    [field: SerializeField, BoxGroup(Value), Cyberevolver.Unity.MinMaxRange(0f, 20f)]
    public float MovementSpeed { get; set; } = 0.11f;

    [field: SerializeField, BoxGroup(Value), Cyberevolver.Unity.MinMaxRange(0f, 400f)]
    public float JumpMultiple { get; private set; } = 1;

    //reference

    [SerializeField, BoxGroup(Reference)]
    private FreezeMenu gameOverManager = null;

    [SerializeField, BoxGroup(Reference), RequiresAny]
    private Transform startRespPoint = null;
    [SerializeField, BoxGroup(Reference)]
    private Transform deathYPoint = null;
    [SerializeField, BoxGroup(Reference)]
    private VisualEffect walkingEffect = null;
    [SerializeField, RequiresAny, BoxGroup(Reference)]
    private Camera cam = null;
    [SerializeField, RequiresAny, BoxGroup(Reference)]
    private Cinemachine.CinemachineVirtualCamera virtualCam = null;
    [SerializeField, BoxGroup(Reference)]
    private AudioSource musicSource = null;
    [SerializeField, BoxGroup(Reference), RequiresAny]
    private Collider2D footCollider = null;

    //assets

    [SerializeField, BoxGroup(Asset)]
    private AudioClip jumpSound = null,
                        gameOverSound = null,
                        walkSound = null,
                        glitchSound = null,
                        repairSound = null;

    [SerializeField, BoxGroup(Asset)]
    private GameObject deathParticle = null;


    //properties

    public bool IsDeath { get; private set; } = false;
    public float PrefferedCameraZoom { get; set; } = 0;

    public bool KeysReversed { get; set; } = false;

    private InputActions inputActions;
    private Vector2 movement;



    [Auto]
    public SpriteRenderer Sprite { get; private set; }
    [Auto]
    public AudioSource Source { get; private set; }
    [Auto]
    public Animator Animator { get; private set; }
    [Auto]
    public Rigidbody2D Rgb { get; private set; }

    public ReadOnlyCollection<GlitchEffect> CurrentGlithes => new ReadOnlyCollection<GlitchEffect>(currentGlitches);
    public float SceneTime => Time.time - timeOnStart;

    public Cinemachine.CinemachineVirtualCamera Virtual => virtualCam;
    public Camera Cam => cam;
    public Transform StartRespPoint => startRespPoint;
    public Collider2D Foot => footCollider;

    public event EventHandler OnPlayerDeath = delegate { };

    private readonly List<GlitchEffect> currentGlitches = new List<GlitchEffect>();

    private bool canJump = false;
    private float move = 0f;
    private bool isWalkSound = false;
    private float timeOnStart;
    private bool jumped = false;

    private new void Awake()
    {
        base.Awake();
        inputActions = new InputActions();
        inputActions.PlayerControls.Move.performed += ctx => movement = ctx.ReadValue<Vector2>();
    }

    private void OnEnable()
    {
        inputActions.Enable();
    }

    private void OnDisable()
    {
        inputActions.Disable();
    }

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
#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
#endif
        if (IsDeath)
        {
            return;
        }
        if (this.transform.position.y < deathYPoint.position.y)
        {
            this.Kill();
            return;
        }


        foreach (var item in currentGlitches)
        {
            item.Update();
        }


        move = GetProperMovement(KeysReversed);


        bool isFlip = move < 0;
        move *= MovementSpeed;
        walkingEffect.SetBool("Spawn", move != 0);
        walkingEffect.SetBool("Flip", isFlip);
        this.Sprite.flipX = isFlip;

        if (this.Rgb.velocity.y < -1.3f)
            Animator.SetInteger(AnimatorValueName, (int)AnimState.Faling);

        else if (move == 0)
            Animator.SetInteger(AnimatorValueName, (int)AnimState.Idle);
        else
            Animator.SetInteger(AnimatorValueName, (int)AnimState.Walking);

        if (inputActions.PlayerControls.OpenPause.triggered)
        {
            GameObject.FindGameObjectWithTag("PauseManager").GetComponent<FreezeMenu>().MenuOpen();
        }

        if (inputActions.PlayerControls.Jump.triggered)
        {
            jumped = true;
        }
    }

    private float GetProperMovement(bool isReversed)
    {
        if (isReversed)
        {
            return movement.x * (-1);
        }

        else
        {
            return movement.x;
        }
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("Ground"))
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

        if ((int)(virtualCam.m_Lens.FieldOfView) != (int)(PrefferedCameraZoom))
            virtualCam.m_Lens.FieldOfView += (PrefferedCameraZoom - virtualCam.m_Lens.FieldOfView) / 3;

        Move();

        if (jumped == true && canJump == true)
        {
            Jump();
            jumped = false;
        }
    }

    public void ClearRandomEffect()
    {
        if (currentGlitches.Count != 0)
        {
            var index = UnityEngine.Random.Range(0, currentGlitches.Count);
            currentGlitches[index].Cancel();
            currentGlitches.RemoveAt(index);
        }
    }
    public void ClearAllGlitches()
    {
        foreach (var item in currentGlitches)
        {
            item.Cancel();
        }
        currentGlitches.Clear();

    }

    public void PushBugs(GlitchEffect effect)
    {
        if (this.IsDeath)
            return;
        GameManager.Instance.AddScore(scoreByBugs);
        Source.PlayOneShot(glitchSound);
        if (effect == null)
            return;
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
            IsDeath = true;
            move = 0;
            walkingEffect.SetBool("Spawn", false);
            musicSource.Stop();
            Source.PlayOneShot(gameOverSound);
            StartCoroutine(DeathProcess());
        }

    }

    private IEnumerator DeathProcess()
    {

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



    private void Jump()
    {
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
