using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
public class PlayerController2D : MonoBehaviour
{
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

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
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

    // ─────────────────────────────
    // Input
    // ─────────────────────────────
    void HandleInput()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        isRunning = Input.GetKey(KeyCode.LeftShift);
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
    // Movement (只控制 X)
    // ─────────────────────────────
    void Move()
    {
        if (isTouchingWall && horizontal != 0 &&
            Mathf.Sign(horizontal) == -Mathf.Sign(wallNormalX))
        {
            rb.linearVelocity = new Vector2(0f, rb.linearVelocity.y);
            return;
        }

        float targetSpeed = 0f;

        if (horizontal != 0)
        {
            float baseSpeed = isRunning ? runSpeed : walkSpeed;
            targetSpeed = horizontal * baseSpeed;
        }

        float speedDiff = targetSpeed - rb.linearVelocity.x;
        float accelRate = Mathf.Abs(targetSpeed) > 0.01f ? acceleration : deceleration;

        float movement = Mathf.MoveTowards(
            rb.linearVelocity.x,
            targetSpeed,
            accelRate * Time.fixedDeltaTime
        );

        rb.linearVelocity = new Vector2(
            movement,
            rb.linearVelocity.y
        );
    }

    // ─────────────────────────────
    // Collision (Ground & Wall)
    // ─────────────────────────────
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
        }
    }
}
