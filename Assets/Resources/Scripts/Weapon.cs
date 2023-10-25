using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    //Based on the tutorial from: https://www.youtube.com/watch?v=LqrAbEaDQzc&t=2s&ab_channel=BMo
    [Header("Weapon Settings")]
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float bulletSpeed = 10f;
    public float attackSpeed = 1f;
    bool OnCooldown = false; //Cooldown for attacks
    void Update()
    {
            //Check for mouse input 
            if (Input.GetButtonDown("Fire1") && !OnCooldown) //Has to wait for cooldown
            {
                Fire();
                StartCoroutine(Cooldown());
            }
    }

    private IEnumerator Cooldown() //Cooldown coroutine for the staff
   {
    OnCooldown = true; //Starts a cooldown
    yield return new WaitForSeconds(attackSpeed); //Set the cooldown time 
    OnCooldown = false; //Reset the cooldown
   }

    void Fire()
{  
    Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition); //Get the cursor of the mouse in the world/game space
    Vector3 aimDirection = (mousePosition - firePoint.position); //Calculate the direction from the firePoint to the mouse cursor and normalizing it 
    aimDirection.Normalize(); //Making the bullet speed constant no matter the distance of the cursor in-game
    float aimAngle = Mathf.Atan2(aimDirection.y, aimDirection.x); //calculating the angle between the 2 directions x,y
    GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.Euler(0f, 0f, aimAngle * Mathf.Rad2Deg)); //Creating a bullet with the rotation around Z axis(pointing towards the screen) using Quaternion Euler angles (x,y is set to 0)
    Rigidbody2D body = bullet.GetComponent<Rigidbody2D>();
    Vector2 velocity = new Vector2(Mathf.Cos(aimAngle), Mathf.Sin(aimAngle)) * bulletSpeed; //Calculate the velocity using the aim angle and bullet speed
    body.velocity = velocity;

    Destroy(bullet, 2f);
}
}
