using UnityEngine;

public class PlayerController2D : MonoBehaviour
{
    [Header("Movement")]
    public float walkSpeed = 5f;
    public float runSpeed = 8f;
    public float jumpForce = 12f;

    [Header("Gravity")]
    public float gravity = -25f;
    public float fallMultiplier = 1.5f;

    [Header("Ground Check (Ray)")]
    public float groundCheckDistance = 0.25f;
    public LayerMask groundLayer;

    private float horizontal;
    private float verticalVelocity;
    private bool isGrounded;
    private bool isRunning;
    CapsuleCollider2D col;


    void Awake()
    {
        col = GetComponent<CapsuleCollider2D>();
    }

    void Update()
    {
        HandleInput();
        GroundCheck();
        HandleJump();
        ApplyGravity();
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
    // Ground Check & Stick to Ground
    // ─────────────────────────────
    void GroundCheck()
    {
        // collider 底部中心
        Vector2 origin = new Vector2(
            col.bounds.center.x,
            col.bounds.min.y + 0.01f   // 稍微抬一点，防止刚好贴边
        );

        RaycastHit2D hit = Physics2D.Raycast(
            origin,
            Vector2.down,
            groundCheckDistance,
            groundLayer
        );

        if (hit.collider != null)
        {
            isGrounded = true;

            if (verticalVelocity <= 0)
            {
                transform.position = new Vector3(
                    transform.position.x,
                    hit.point.y + col.bounds.extents.y,
                    transform.position.z
                );

                verticalVelocity = 0f;
            }
        }
        else
        {
            isGrounded = false;
        }
    }


    // ─────────────────────────────
    // Jump
    // ─────────────────────────────
    void HandleJump()
    {
        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            verticalVelocity = jumpForce;
            isGrounded = false;
        }
    }

    // ─────────────────────────────
    // Gravity
    // ─────────────────────────────
    void ApplyGravity()
    {
        // 在地面且未起跳时，不施加重力
        if (isGrounded && verticalVelocity <= 0)
            return;

        if (verticalVelocity < 0)
        {
            verticalVelocity += gravity * fallMultiplier * Time.deltaTime;
        }
        else
        {
            verticalVelocity += gravity * Time.deltaTime;
        }
    }

    // ─────────────────────────────
    // Movement
    // ─────────────────────────────
    void Move()
    {
        float speed = isRunning ? runSpeed : walkSpeed;

        Vector3 movement = new Vector3(
            horizontal * speed,
            verticalVelocity,
            0f
        ) * Time.deltaTime;

        transform.Translate(movement);
    }

}
