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

    public float groundedCheckDistance = 0.1f;
    public LayerMask groundLayer;

    public Transform spawner;

    [Header("Audio")]
    public RandomAudioPlayer jumpAudio;
    public RandomAudioPlayer doubleJumpAudio;
    public RandomAudioPlayer footstepsAudio;

    public bool IsGrounded { get; private set; }
    public bool IsRunning { get; private set; }
    public bool RequestingJump { get; private set; }

    Animator anim;
    Rigidbody2D body;

    void Awake()
    {
        anim = GetComponent<Animator>();
        body = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        HandleInput();
        CheckIfGrounded();
        print(RequestingJump);
    }

    private void FixedUpdate()
    {
        MoveForward();
        SetAnimator();

        if (RequestingJump && IsGrounded)
        {
            Jump();
        }
        else
        {
            RequestingJump = false;
        }

        if (Mathf.Abs(body.velocity.x) > 0f && IsGrounded)
        {
            IsRunning = true;
        }

        // Adjust the gravity scale so jump feels less floaty
        if (body.velocity.y < 0f)
        {
            body.gravityScale = fallMultiplier;
        }
        else if (body.velocity.y > 0f)
        {
            body.gravityScale = lowJumpMultiplier;
        }
        else
        {
            body.gravityScale = 1f;
        }
    }

    void HandleInput()
    {
        if (Input.anyKeyDown)
        {
            RequestingJump = true;
        }
    }

    void MoveForward()
    {
        body.velocity = new Vector2(1f * moveSpeed, body.velocity.y);
    }

    void Jump()
    {
        if (IsGrounded)
        {
            IsGrounded = false;
            RequestingJump = false;
            body.AddForce(Vector2.up * jumpVelocity, ForceMode2D.Impulse);
            jumpAudio.PlayRandomSound();
        }
    }

    void CheckIfGrounded()
    {
        Vector2 rayStart = transform.position;
        IsGrounded = Physics2D.Raycast(rayStart, Vector2.down,
            groundedCheckDistance, groundLayer) ? true : false;

        // TODO: Add some kind of in-game debug that allows this stuff to be turned on and off
        Debug.DrawRay(rayStart, Vector2.down * groundedCheckDistance,
            new Color(1, 0, 0));
    }

    void SetAnimator()
    {
        anim.SetBool("IsGrounded", IsGrounded);
        anim.SetBool("IsRunning", IsRunning);
    }

    /// <summary>
    /// Respawner
    /// </summary>
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "Goal")
            gameObject.transform.position = spawner.position;
    }
}
