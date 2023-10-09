using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float bulletSpeed = 10f;

    void Update()
    {
        // Check for mouse input (e.g., left mouse button click).
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        // Calculate the direction from the player to the mouse cursor.
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 fireDirection = (mousePosition - (Vector2)firePoint.position).normalized;

        // Create a bullet instance at the fire point.
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);

        // Activate the bullet GameObject.
        bullet.SetActive(true);

        // Get the bullet's Rigidbody2D component and set its velocity.
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.velocity = fireDirection * bulletSpeed;

        // Destroy the bullet after a certain time (e.g., 2 seconds).
        Destroy(bullet, 2f);
    }
}