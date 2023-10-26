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
    SpriteRenderer character; //for flipping the sprite
    private bool isAlive = true;

    private float nextDamageTime = 0f;  // The time when the player can take damage again.
    public float damageCooldown = 1.0f; // The cooldown time between taking damage (adjust as needed).

    [Header("Health System")]
    public int maxHealth = 100;
    public int currentHealth;
    [SerializeField] StatusBar hpBar;

    [Header("Damage System")]
    public int attackDamage = 10;
    public float attackSpeed = 1.0f;

    private Rigidbody2D body;
    private Vector2 targetVelocity; 
    

    //Footstep sound from: https://pixabay.com/sound-effects/search/footsteps/
    [Header("Audio Source")]
    [SerializeField] private AudioSource walkSound;
    private bool isWalking = false;
    
    //Set current health of player to max health at the start and get Rigidbody Component for the enemies
    void Awake() //Calls it when the scene is first initialized 
    {
        body = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
        targetVelocity = Vector2.zero;
        character = this.GetComponent<SpriteRenderer>(); //getting sprite component
        
    }
    
    void FixedUpdate()
    {
        //Getting inputs for horizontal and vertical movement
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        
        Flip(horizontalInput);
        


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

        if (walkSound != null)
        {
            if (horizontalInput != 0)
            {
                if (!isWalking)
                {
                    walkSound.Play();
                    isWalking = true; // Set the flag to true when walking starts
                }
            }
            else
            {
                if (isWalking)
                {
                    walkSound.Pause();
                    isWalking = false; // Set the flag to false when walking stops
                }
            }
        }
        
    }

    public void TakeDamage(int damage)
    {
        if (currentHealth > 0) 
        {
            currentHealth -= damage;
            animator.SetTrigger("IsHit");
            //Check if the player is dead
            if (currentHealth <= 0)
            {
                Die();
            }
            hpBar.SetState(currentHealth, maxHealth);
        }
    }

    public float GetNextDamageTime() //reference to enemycontroller
    {
        return nextDamageTime;
    }

    public void Heal(int heal)
    {

        if (currentHealth <= 0) { return; }
        currentHealth += heal;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
    }

     void Flip(float horizontalInput) //Flip method with direction value
    {

        character.flipX = horizontalInput < 0; //uses character sprite flip when the entity is moving to the left (if horizontal input is < 0)
        
        //From Naosse's Class
        //Flip the characters sprite
        /*Vector3 currentScale = transform.localScale;
        currentScale.x *= -1;
        transform.localScale = currentScale;

        //Update the facing direction
        facingRight = !facingRight;*/
    }

    void Die()
{
        isAlive = false;
        //Trigger the "Die" animation
        animator.SetTrigger("Die");
        
}

public void DestroyObject()
{
    //Destroy the game object after calling for animator event
    Destroy(gameObject);
}

//returns alive status to enemy script
public bool IsPlayerAlive()
    {
        return isAlive;
    }
}