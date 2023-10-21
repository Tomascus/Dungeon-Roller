using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Player Settings")]
    public float speed = 5f; //Controls for adjusting the move speed inside the game
    public float acceleration = 10f; //Controls for adjusting the acceleration for smoother movement inside the game
    public Animator animator;

    private float nextDamageTime = 0f;  // The time when the player can take damage again.
    public float damageCooldown = 1.0f; // The cooldown time between taking damage (adjust as needed).

    [Header("Health System")]
    public int maxHealth = 1000;
    public int currentHealth;

    [Header("Damage System")]
    public int attackDamage = 10;
    public float attackSpeed = 1.0f;

    private Rigidbody2D body;
    private Vector2 targetVelocity; 
    private bool facingRight = true; //for fliping the character 
    
    //Set current health of player to max health at the start and get Rigidbody Component for the enemies
    void Start() //Calls it when the scene is first initialized 
    {
        body = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
        targetVelocity = Vector2.zero;
        
    }
    
    void FixedUpdate()
    {
        //Getting inputs for horizontal and vertical movement
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

         if ((horizontalInput>0 && !facingRight)||(horizontalInput<0 && facingRight))
        {
            Flip();
        }


        //Creating input velocity calculation for basic movement and then inputing it for final targetVelocity which adds acceleration calculation
        Vector2 inputVelocity = new Vector2(horizontalInput, verticalInput).normalized * speed;
        targetVelocity = Vector2.Lerp(targetVelocity, inputVelocity, Time.deltaTime * acceleration);

        //Applying smoother movement with acceleration to a rigidbody
        body.velocity = targetVelocity;


        //Followed tutorial: https://www.youtube.com/watch?v=hkaysu1Z-N8&t=1s&ab_channel=Brackeys
        //Calculate the magnitude(strength) of input to determine speed
        float inputMagnitude = new Vector2(horizontalInput, verticalInput).magnitude;

        //Check if the magnitude is greater than 0.01 and if so, play animation
        if (inputMagnitude > 0.01f)
        {
            //Set the "Speed" parameter in the Animator
            animator.SetFloat("Speed", inputMagnitude);
        }
        else
        {
            //If the magnitude is 0 (Character is not moving), stop the animation
            animator.SetFloat("Speed", 0);
        }
        
    }

    public void TakeDamage(int damage)
    {
        if (currentHealth > 0) //Here we compare the time elapsed and check if it is bigger than nextDamageTime which is calculated by adding 1s cooldown to time, essentailly making 1s wait between attacks
        {
            currentHealth -= damage;
            //nextDamageTime = Time.time + damageCooldown; //adding 1s to total time as nextDamageTime
            //Check if the player is dead
            if (currentHealth <= 0)
            {
                Die();
            }
        }
    }

    public float GetNextDamageTime() //reference to enemycontroller
    {
        return nextDamageTime;
    }

    //From Naosee's class
    private void Flip()
    {
        Vector3 currentScale = gameObject.transform.localScale;
        currentScale.x *= -1;
        gameObject.transform.localScale = currentScale;
        facingRight = !facingRight;
    }

    void Die()
{
   
        //Trigger the "Die" animation
        animator.SetTrigger("Die");
        
}

public void DestroyObject()
{
    //Destroy the game object after calling for animator event
    Destroy(gameObject);
}
}