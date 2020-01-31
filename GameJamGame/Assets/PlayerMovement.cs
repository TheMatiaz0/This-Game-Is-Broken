using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cyberevolver.Unity;

public class PlayerMovement : MonoBehaviourPlus
{
    [field: SerializeField, Cyberevolver.Unity.MinMaxRange(0f, 20f)]
    public float MovementSpeed { get; private set; } = 0.11f;

    [field: SerializeField, Cyberevolver.Unity.MinMaxRange(0f, 400f)]
    public float JumpMultiple { get; private set; }

    [Auto]
    public Rigidbody2D Rb2D { get; set; }

    private float move;

    protected void Update()
    {
        move = Input.GetAxisRaw("Horizontal") * MovementSpeed;
    }

    protected void FixedUpdate()
    {
        Move();

        if (Input.GetButtonDown("Jump"))
        {
            Jump();
        }
    }

    private void Jump ()
    {
        Rb2D.velocity = new Vector2(Rb2D.velocity.x, Vector3.up.y * JumpMultiple);
    }

    private void Move ()
    {
        Rb2D.velocity = new Vector2(move * Vector2.right.x, Rb2D.velocity.y);
    }
}
