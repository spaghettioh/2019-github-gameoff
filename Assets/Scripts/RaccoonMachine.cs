using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaccoonMachine : ByTheTale.StateMachine.MachineBehaviour
{
    public override void AddStates()
    {
        AddState<RaccoonStateIdle>();
        AddState<RaccoonStateRun>();
        AddState<RaccoonStateInAir>();
        AddState<RaccoonStateWallSlide>();

        SetInitialState<RaccoonStateIdle>();
    }

    [Header("Movement")]
    public float moveForce = 3.0f;
    public float jumpForce = 5.0f;
    [Tooltip("Attempts to get rid of floaty jumping.")]
    public float fallMultiplier = 2.5f;
    [Tooltip("Allows player to release button mid-jump for a short hop.")]
    public float lowJumpMultiplier = 2.0f;

    [Header("Physics stuff")]
    public Transform backFeet;
    public Transform frontFeet;
    public float surfaceCheckDistance = 0.1f;
    public LayerMask groundLayer;
    public Transform frontGrip;
    public BooleanVariable IsGrounded;
    public BooleanVariable IsAgainstWall;
    public BooleanVariable IsWallSliding;

    //public bool IsAgainstWall { get; private set; }

    [Header("Audio")]
    public RandomAudioPlayer jumpAudio;
    public RandomAudioPlayer wallJumpAudio;
    public RandomAudioPlayer doubleJumpAudio;
    public RandomAudioPlayer footstepsAudio;

    //public bool IsRunning { get; private set; }
    public bool RequestingJump { get; set; }

    public int direction = 1;
    public Animator anim;
    public Rigidbody2D body;
    public bool pushing;

    [Header("Debug")]
    [SerializeField] bool debug_grounded;
    [SerializeField] bool debug_againstWall;
    [SerializeField] bool debug_sliding;
    [SerializeField] float debug_xVelocity;
    [SerializeField] float debug_yVelocity;
    [SerializeField] bool debug_pushing;
    [SerializeField] bool debug_RequestingJump;
    float slomo = .5f;

    public void Awake()
    {
        anim = GetComponent<Animator>();
        body = GetComponent<Rigidbody2D>();

        // Reset all the things
        pushing = false;
        RequestingJump = false;
        IsGrounded.value = false;
        IsAgainstWall.value = false;
        IsWallSliding.value = false;
    }


    public override void Update()
    {
        base.Update();

        CheckIfGrounded();
        CheckIfAgainstWall();

        // DEBUG STUFF
        debug_grounded = IsGrounded.value;
        debug_againstWall = IsAgainstWall.value;
        debug_sliding = IsWallSliding.value;
        debug_xVelocity = (float)System.Math.Round(body.velocity.x, 2);
        debug_yVelocity = (float)System.Math.Round(body.velocity.y, 2);
        debug_pushing = pushing;
        debug_RequestingJump = RequestingJump;

        // Useful for troubleshooting
        //if (Input.GetKeyDown(KeyCode.Q))
        //{
        //    slomo = .1f;
        //}
        //if (Input.GetKeyDown(KeyCode.W))
        //{
        //    slomo = .5f;
        //}

        //Time.timeScale = slomo;
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();

        if (pushing)
        {
            // Always blue. Always blue. Always move. Always move right.
            body.AddForce(Vector2.right * moveForce, ForceMode2D.Force);
        }

        transform.localScale = new Vector3(1 * direction, 1, 1);
    }

    public void GroundedJump()
    {
        RequestingJump = false;
        IsGrounded.SetValue(false);
        body.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        RequestingJump = false;
        jumpAudio.PlayRandomSound();

        ChangeState<RaccoonStateInAir>();
    }

    void CheckIfGrounded()
    {
        Vector2 backFeetStart = backFeet.position;
        Vector2 frontFeetStart = frontFeet.position;
        bool groundedResult = (Physics2D.Raycast(backFeetStart, Vector2.down,
            surfaceCheckDistance, groundLayer) ||
            Physics2D.Raycast(frontFeetStart, Vector2.down,
            surfaceCheckDistance, groundLayer));
        IsGrounded.SetValue(groundedResult || false);

        // TODO: Add some kind of in-game debug that allows this stuff to be turned on and off
        Debug.DrawRay(backFeetStart, Vector2.down * surfaceCheckDistance,
            new Color(1, 0, 0));
        Debug.DrawRay(frontFeetStart, Vector2.down * surfaceCheckDistance,
            new Color(1, 0, 0));
    }

    void CheckIfAgainstWall()
    {
        Vector2 rayStart = frontGrip.position;
        IsAgainstWall.value = Physics2D.Raycast(rayStart, Vector2.right * direction,
            surfaceCheckDistance, groundLayer) || false;

        // TODO: Add some kind of in-game debug that allows this stuff to be turned on and off
        Debug.DrawRay(rayStart, Vector2.right * direction * surfaceCheckDistance,
            new Color(0, 1, 0));
    }

    public virtual void SetAnimator(string clip)
    {
        anim.SetTrigger(clip);
    }
}
