using UnityEngine;

public class PlayerRotation : MonoBehaviour
{
    public Transform playerTransform; // Reference to the player's Transform component.
    public float rotationSpeed = 5f; // Adjust this value to control the rotation speed.

    void Update()
    {
        // Get the mouse position in world coordinates.
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // Calculate the direction vector from the player to the mouse.
        Vector2 direction = (mousePosition - playerTransform.position).normalized;

        // Calculate the angle in degrees.
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Rotate the player towards the mouse cursor smoothly.
        Quaternion targetRotation = Quaternion.Euler(0f, 0f, angle);
        playerTransform.rotation = Quaternion.Slerp(playerTransform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }
}