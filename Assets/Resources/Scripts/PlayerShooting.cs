using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float bulletSpeed = 10f;

    void Update()
    {
        //Check for mouse input 
        if (Input.GetButtonDown("Fire1"))
        {
            FireBullet();
        }
    }

    void FireBullet()
{
   
    Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

    
    Vector3 aimDirection = (mousePosition - firePoint.position).normalized;

    
    float angle = Mathf.Atan2(aimDirection.y, aimDirection.x);

    
    GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.Euler(0f, 0f, angle * Mathf.Rad2Deg));

    //THIS COULD BE CHANGED, basic solution for now
    bullet.SetActive(true);

    
    Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
    rb.velocity = aimDirection * bulletSpeed;

    Destroy(bullet, 2f);
}
}