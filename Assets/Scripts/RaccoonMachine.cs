using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    public float jumpModifier = 0;
    public FloatVariable inputHoldTimer;
    public Slider jumpHoldProgressBar;
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
    public bool push;

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
        push = false;
        RequestingJump = false;
        IsGrounded.value = false;
        IsAgainstWall.value = false;
        IsWallSliding.value = false;
    }


    public override void Update()
    {
        base.Update();

        SetJumpMultiplier();
        

        // DEBUG STUFF
        debug_grounded = IsGrounded.value;
        debug_againstWall = IsAgainstWall.value;
        debug_sliding = IsWallSliding.value;
        debug_xVelocity = (float)System.Math.Round(body.velocity.x, 2);
        debug_yVelocity = (float)System.Math.Round(body.velocity.y, 2);
        debug_pushing = push;
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

        CheckIfGrounded();
        CheckIfAgainstWall();

        if (push && body.velocity.x < moveForce)
        {
            if (body.velocity.x < 0)
            {

                body.AddForce(Vector2.right * moveForce * 2, ForceMode2D.Force);
            }
            else
            {
                // Always blue. Always blue. Always move. Always move right.
                body.AddForce(Vector2.right * moveForce, ForceMode2D.Force);

            }
        }

        transform.localScale = new Vector3(1 * direction, 1, 1);
    }

    public void GroundedJump()
    {
        RequestingJump = false;
        IsGrounded.SetValue(false);
        body.AddForce(Vector2.up * (jumpForce + jumpModifier), ForceMode2D.Impulse);
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

    public virtual void SetJumpMultiplier()
    {
        float t = inputHoldTimer.value;
        float inputHoldNormalized = 0;

        if (t > 0 && t < 1)
        {
            inputHoldNormalized += t;

        }
        else if (t > 1 && t < 2)
        {
            inputHoldNormalized = 2 - t;
        }

        jumpHoldProgressBar.value = inputHoldNormalized;

        jumpModifier = inputHoldNormalized;
    }

    public virtual void SetAnimator(string clip)
    {
        if (!anim.GetCurrentAnimatorStateInfo(0).IsName(clip))
        {
            anim.SetTrigger(clip);

        }
    }
}
