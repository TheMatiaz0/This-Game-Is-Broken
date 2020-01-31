using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cyberevolver.Unity;

public class PlayerMovement : MonoBehaviourPlus
{
    [field: SerializeField, Cyberevolver.Unity.MinMaxRange(0f, 0.8f)]
    public float MovementSpeed { get; private set; } = 0.11f;

    [field: SerializeField, Cyberevolver.Unity.MinMaxRange(0f, 400f)]
    public float JumpMultiple { get; private set; }

    [Auto]
    public Rigidbody2D Rb2D { get; set; }

    private float move;

    protected void Update()
    {
        move = Input.GetAxisRaw("Horizontal") * MovementSpeed;


        if (Input.GetButtonDown("Jump"))
        {
            Jump();
        }
    }

    private void Jump ()
    {
        Rb2D.velocity = Vector3.up * JumpMultiple;

        // Rb2D.AddForce(Vector3.up * JumpMultiple);
    }

    protected void FixedUpdate()
    {
        // returns -1 to 1 multiplied by speed
        Rb2D.MovePosition((Vector2)transform.position + move * Vector2.right);
    }
}
