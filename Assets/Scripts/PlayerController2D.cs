using UnityEngine;

public class PlayerController2D : MonoBehaviour
{
    [Header("Character")]
    private Vector2 spawnPosition;

    [Header("Movement")]
    public float walkSpeed = 5f;
    public float runSpeed = 8f;
    public float jumpForce = 12f;

    [Header("Horizontal Acceleration")]
    public float acceleration = 40f;
    public float deceleration = 60f;

    private float horizontal;
    private bool isRunning;
    private bool isGrounded;

    private Rigidbody2D rb;
    private Collider2D col;

    private bool isTouchingWall;
    private float wallNormalX;

    [Header("Fast Fall")]
    public float fastFallDelay = 0.5f;
    public float fastFallMultiplier = 2.5f;
    public float maxFallSpeed = -25f;
    private float gravityScale;

    private FastFallState fastFallState = FastFallState.None;
    private float fastFallTimer;

    enum FastFallState
    {
        None,
        Windup,
        Falling
    }



    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
        gravityScale = rb.gravityScale;

        spawnPosition = rb.position;
    }

    void Update()
    {
        HandleInput();
        HandleJump();
    }

    void FixedUpdate()
    {
        Move();
    }

    void HandleInput()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        isRunning = Input.GetKey(KeyCode.LeftShift);

        if (!isGrounded &&
            fastFallState == FastFallState.None &&
            Input.GetKeyDown(KeyCode.S))
        {
            fastFallState = FastFallState.Windup;
            fastFallTimer = fastFallDelay;
        }
    }


    // ─────────────────────────────
    // Jump
    // ─────────────────────────────
    void HandleJump()
    {
        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            isGrounded = false;
        }
    }

    // ─────────────────────────────
    // Movement
    // ─────────────────────────────
    void Move()
    {
        Vector2 velocity = rb.linearVelocity;

        //stay in the air
        if (fastFallState == FastFallState.Windup)
        {
            velocity.x = 0f;
            velocity.y = 0f;
            rb.gravityScale = 0f;//gravity set to 0

            rb.linearVelocity = velocity;

            fastFallTimer -= Time.fixedDeltaTime;
            if (fastFallTimer <= 0f)
            {
                fastFallState = FastFallState.Falling;
            }
            return; 
        }
        //falling
        if (fastFallState == FastFallState.Falling)
        {
            velocity.x = 0f;
            rb.gravityScale = gravityScale;

            if (velocity.y <= 0f)
            {
                velocity.y = Mathf.Max(
                    velocity.y * fastFallMultiplier,
                    maxFallSpeed
                );
            }

            rb.linearVelocity = velocity;
            return; 
        }
        float targetSpeed = 0f;

        if (horizontal != 0)
        {
            float baseSpeed = isRunning ? runSpeed : walkSpeed;
            targetSpeed = horizontal * baseSpeed;
        }

        float accelRate = Mathf.Abs(targetSpeed) > 0.01f
            ? acceleration
            : deceleration;

        float newX = Mathf.MoveTowards(
            rb.linearVelocity.x,
            targetSpeed,
            accelRate * Time.fixedDeltaTime
        );

        rb.linearVelocity = new Vector2(
            newX,
            rb.linearVelocity.y
        );
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        EvaluateCollision(collision);
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        EvaluateCollision(collision);
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        isGrounded = false;
        isTouchingWall = false;
        wallNormalX = 0f;
    }

    void EvaluateCollision(Collision2D collision)
    {
        foreach (ContactPoint2D contact in collision.contacts)
        {
            if (contact.normal.y > 0.5f)
            {
                isGrounded = true;
            }

            if (Mathf.Abs(contact.normal.x) > 0.5f)
            {
                isTouchingWall = true;
                wallNormalX = contact.normal.x;
            }
            if (contact.normal.y > 0.5f)
            {
                isGrounded = true;
                fastFallState = FastFallState.None;
                rb.gravityScale = gravityScale;

            }
            if(collision.transform.tag == "DeadBox")
            {
                rb.position = spawnPosition;
            }

        }
    }
}
