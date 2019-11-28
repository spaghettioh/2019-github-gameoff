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
    public float moveSpeed = 4;
    public FloatVariable moveSpeedMultiplierStat;
    public float jumpForce = 8;
    public FloatVariable jumpMultiplierStat;
    // A separate variable is needed because it also checks the input hold meter
    public float jumpMultiplier;
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

    // F7 to turn on/off
    public float slomoSpeed = .5f;
    public bool slomoOnOff;

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
        moveSpeed *= moveSpeedMultiplierStat.value;
        
        // ======= DEBUG STUFF
        debug_grounded = IsGrounded.value;
        debug_againstWall = IsAgainstWall.value;
        debug_sliding = IsWallSliding.value;
        debug_xVelocity = (float)System.Math.Round(body.velocity.x, 2);
        debug_yVelocity = (float)System.Math.Round(body.velocity.y, 2);
        debug_pushing = push;
        debug_RequestingJump = RequestingJump;

        if (Input.GetKeyDown(KeyCode.F7))
        {
            slomoOnOff = !slomoOnOff;
        }
        if (slomoOnOff)
        {
            Time.timeScale = slomoSpeed;
        }
        // ======== DEBUG STUFF
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();

        CheckIfGrounded();
        CheckIfAgainstWall();

        if (body.velocity.y < -0.01f)
        {
            body.gravityScale = fallMultiplier;
        }
        else if (body.velocity.y > 0.1f)
        {
            body.gravityScale = lowJumpMultiplier;
        }
        else
        {
            body.gravityScale = 1;
        }

        // TODO: Can this be simplied to a lerp?
        if (push)
        {
            if (body.velocity.x < moveSpeed)
            {
                if (body.velocity.x < 0)
                {
                    // Force the raccoon back to the right.
                    body.AddForce(Vector2.right * moveSpeed * 2.5f, ForceMode2D.Force);
                }
                else
                {
                    // Always blue. Always blue. Always move. Always move right.
                    body.AddForce(Vector2.right * moveSpeed, ForceMode2D.Force);

                }
            }

            // Slow the raccon if moving too quickly
            if (body.velocity.x > moveSpeed)
            {
                body.drag = 5;
            }
            else
            {
                body.drag = 0;
            }
        }

        // Set the raccoon direction
        transform.localScale = new Vector3(1 * direction, 1, 1);
    }

    // TODO: Refactor this to be generic for any jump request
    public void GroundedJump()
    {
        RequestingJump = false;
        IsGrounded.SetValue(false);
        body.velocity += Vector2.up * (jumpForce * jumpMultiplier);
        //body.AddForce(Vector2.up * (jumpForce + jumpModifier), ForceMode2D.Impulse);
        RequestingJump = false;
        jumpAudio.PlayRandomSound();

        ChangeState<RaccoonStateInAir>();
    }

    void CheckIfGrounded()
    {
        Vector2 backFeetStart = backFeet.position;
        Vector2 frontFeetStart = frontFeet.position;
        bool groundedResult = Physics2D.Raycast(backFeetStart, Vector2.down, surfaceCheckDistance, groundLayer) ||
                              Physics2D.Raycast(frontFeetStart, Vector2.down, surfaceCheckDistance, groundLayer);
        IsGrounded.SetValue(groundedResult || false);

        Debug.DrawRay(backFeetStart, Vector2.down * surfaceCheckDistance,
            new Color(1, 0, 0));
        Debug.DrawRay(frontFeetStart, Vector2.down * surfaceCheckDistance,
            new Color(1, 0, 0));
    }

    void CheckIfAgainstWall()
    {
        Vector2 rayStart = frontGrip.position;
        bool againstWallResult = Physics2D.Raycast(rayStart, Vector2.right * direction, surfaceCheckDistance, groundLayer);
        IsAgainstWall.SetValue(againstWallResult || false);

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

        if (inputHoldNormalized > 0)
        {
            jumpHoldProgressBar.gameObject.SetActive(true);
            jumpHoldProgressBar.value = inputHoldNormalized;
        }
        else
        {
            jumpHoldProgressBar.gameObject.SetActive(false);
        }

        jumpMultiplier = (jumpMultiplierStat.value + (inputHoldNormalized * .5f));
    }

    public virtual void SetAnimator(string clip)
    {
        if (!anim.GetCurrentAnimatorStateInfo(0).IsName(clip))
        {
            anim.SetTrigger(clip);
        }
    }
}
