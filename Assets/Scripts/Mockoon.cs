using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mockoon : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 3.0f;
    //[Range(0f, 10f)]
    public float jumpVelocity = 5.0f;
    [Tooltip("Attempts to get rid of floaty jumping.")]
    //[Range(0f, 10f)]
    public float fallMultiplier = 2.5f;
    [Tooltip("Allows player to release button mid-jump for a short hop.")]
    //[Range(0f, 10f)]
    public float lowJumpMultiplier = 2.0f;

    public bool IsGrounded { get; private set; }
    public bool IsRunning { get; private set; }
    public bool RequestingJump { get; private set; }

    public float groundedCheckDistance = 0.1f;
    public LayerMask groundLayer;
    public BoxCollider2D collider;
    public Vector2 playerSize;

    public Transform spawner;

    Animator anim;
    Rigidbody2D body;

    void Awake()
    {
        anim = GetComponent<Animator>();
        body = GetComponent<Rigidbody2D>();
        playerSize = collider.size;

        IsGrounded = true;
    }

    void Update()
    {
        HandleInput();
        CheckIfGrounded();
    }

    private void FixedUpdate()
    {
        MoveForward();
        SetAnimator();

        if (RequestingJump && IsGrounded)
            Jump();

        if (body.velocity.x > 0f && IsGrounded)
            IsRunning = true;

        if (body.velocity.y < 0f)
            body.gravityScale = fallMultiplier;
        else if (body.velocity.y > 0f)
            body.gravityScale = lowJumpMultiplier;
        else
            body.gravityScale = 1f;
    }

    void HandleInput()
    {
        if (Input.anyKeyDown)
            RequestingJump = true;
    }

    void MoveForward()
    {
        body.velocity = new Vector2(1f * moveSpeed, body.velocity.y);
    }

    void Jump()
    {
        if (IsGrounded)
            IsGrounded = false;
            body.AddForce(Vector2.up * jumpVelocity, ForceMode2D.Impulse);
            RequestingJump = false;
    }

    void CheckIfGrounded()
    {
        Vector2 rayStart = (Vector2)transform.position;
        IsGrounded = Physics2D.Raycast(rayStart, Vector2.down, groundedCheckDistance, groundLayer);

        Debug.DrawRay(rayStart, Vector2.down * groundedCheckDistance, new Color(1,0,0));
    }

    void SetAnimator()

    {
        anim.SetBool("IsGrounded", IsGrounded);
        anim.SetBool("IsRunning", IsRunning);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "Goal")
            gameObject.transform.position = spawner.position;
    }
}
