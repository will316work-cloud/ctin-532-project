using UnityEngine;

public class TopDownMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f; // Adjust speed in the Inspector
    
    private Rigidbody2D rb;
    private Vector2 movement;

    // Start is called before the first frame update
    void Start()
    {
        // Get the Rigidbody2D component automatically if not assigned
        if (rb == null)
        {
            rb = GetComponent<Rigidbody2D>();
        }
    }

    // Update is called once per frame to capture input
    void Update()
    {
        // Capture input from horizontal and vertical axes
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        // Normalize the movement vector to ensure consistent speed in all directions
        movement.Normalize();
    }

    // FixedUpdate is called at a fixed interval and is ideal for physics operations
    void FixedUpdate()
    {
        // Move the Rigidbody using MovePosition for smooth collision handling
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }
}
