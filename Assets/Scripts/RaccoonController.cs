using UnityEngine;

public class RaccoonController : MonoBehaviour
{
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
    public BooleanVariable IsWallSliding;

    public bool IsAgainstWall { get; private set; }

    [Header("Audio")]
    public RandomAudioPlayer jumpAudio;
    public RandomAudioPlayer wallJumpAudio;
    public RandomAudioPlayer doubleJumpAudio;
    public RandomAudioPlayer footstepsAudio;

    public bool IsRunning { get; private set; }
    public bool RequestingJump { get; private set; }

    int direction = 1;
    Animator anim;
    Rigidbody2D body;

    [Header("Debug")]
    public bool grounded;
    public bool againstWall;
    public bool sliding;
    public float xVelocity;
    public float yVelocity;

    void Start()
    {
        anim = GetComponent<Animator>();
        body = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        CheckIfGrounded();
        CheckIfAgainstWall();

        grounded = IsGrounded.value;
        againstWall = IsAgainstWall;
        sliding = IsWallSliding.value;
        xVelocity = (float)System.Math.Round(body.velocity.x, 2);
        yVelocity = (float)System.Math.Round(body.velocity.y, 2);
    }

    private void FixedUpdate()
    {
        DoPhysics();
        SetAnimator();
        SetSpriteStuff();

        if (RequestingJump)
        {
            Jump();
        }
        else
        {
            RequestingJump = false;
        }

        // Useful for troubleshooting
        //if (!IsGrounded.value)
        //    Time.timeScale = 0.15f;
        //else
        //    Time.timeScale = 1.0f;
    }

    void DoPhysics()
    {
        // Always move right unless in the air
        if (IsGrounded.value)
        {
            body.AddForce(Vector2.right * moveForce, ForceMode2D.Force);
        }
        else
        {
            body.AddForce(Vector2.right * direction * moveForce, ForceMode2D.Force);
        }

        // Adjust the gravity scale so jump feels less floaty
        if (body.velocity.y < -0.01f)
        {
            body.gravityScale = fallMultiplier;
        }
        else if (body.velocity.y > 0.1f && !IsWallSliding.value)
        {
            body.gravityScale = lowJumpMultiplier;
        }
        else
        {
            body.gravityScale = 1;
        }

        // Get ready for wall jump
        if (!IsGrounded.value && IsAgainstWall && body.velocity.y < -0.01f)
        {
            IsWallSliding.value = true;
        }
        else
        {
            IsWallSliding.value = false;
        }

        // Update sprite direction with movement
        if (body.velocity.x < -0.01f)
        {
            direction = -1;
        }
        else
        {
            direction = 1;
        }

        // I hate this garbage
        if ((float)Mathf.Abs(body.velocity.x) > 0.01f)
        {
            IsRunning = true;
        }
        else
        {
            IsRunning = false;
        }
    }

    public void Jump()
    {
        if (IsGrounded.value)
        {
            IsGrounded.SetValue(false);
            body.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            RequestingJump = false;
            //body.velocity += new Vector2(body.velocity.x, jumpVelocity);
            jumpAudio.PlayRandomSound();
        }
        else if (IsWallSliding.value)
        {
            //body.velocity += new Vector2(body.velocity.x, jumpVelocity);
            direction *= -1;
            body.AddForce(new Vector2(moveForce * direction, jumpForce), ForceMode2D.Impulse);
            RequestingJump = false;
            wallJumpAudio.PlayRandomSound();
        }
        else
        {
            RequestingJump = false;
        }
    }

    void SetSpriteStuff()
    {
        // Update sprite direction with movement
        transform.localScale = new Vector3(1 * direction, 1, 1);
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
        IsAgainstWall = Physics2D.Raycast(rayStart, Vector2.right * direction,
            surfaceCheckDistance, groundLayer) || false;

        // TODO: Add some kind of in-game debug that allows this stuff to be turned on and off
        Debug.DrawRay(rayStart, Vector2.right * surfaceCheckDistance,
            new Color(0, 1, 0));
    }

    void SetAnimator()
    {
        anim.SetBool("IsGrounded", IsGrounded.value);
        anim.SetBool("IsRunning", IsRunning);
        anim.SetBool("IsWallSliding", IsWallSliding.value);
    }
}
