using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f; // Adjust this value to control the maximum movement speed.
    public float acceleration = 10f; // Adjust this value to control the acceleration rate.

    private Rigidbody2D rb;
    private Vector2 targetVelocity;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        targetVelocity = Vector2.zero;
    }

    void Update()
    {
        // Get input for horizontal and vertical movement.
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // Calculate the target velocity based on input.
        Vector2 inputVelocity = new Vector2(horizontalInput, verticalInput) * moveSpeed;
        targetVelocity = Vector2.Lerp(targetVelocity, inputVelocity, Time.deltaTime * acceleration);

        // Apply the smoothed movement to the Rigidbody2D.
        rb.velocity = targetVelocity;
    }
}