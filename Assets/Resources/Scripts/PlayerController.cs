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

    [Header("Health System")]
    public int maxHealth = 100;
    public int currentHealth;

    [Header("Damage System")]
    public int attackDamage = 10;
    public float attackSpeed = 1.0f;
    private float nextAttackTime = 0f;

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

    void Update()
    {
        //Getting inputs for horizontal and vertical movement
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

         if ((horizontalInput>0 && !facingRight)||(horizontalInput<0 && facingRight))
        {
            Flip();
        }


        //Creating input velocity calculation for basic movement and then inputing it for final targetVelocity which adds acceleration calculation
        Vector2 inputVelocity = new Vector2(horizontalInput, verticalInput) * speed;
        targetVelocity = Vector2.Lerp(targetVelocity, inputVelocity, Time.deltaTime * acceleration);

        //Applying smoother movement with acceleration to a rigidbody
        body.velocity = targetVelocity;

        //Followed tutorial: https://www.youtube.com/watch?v=hkaysu1Z-N8&t=1s&ab_channel=Brackeys
        animator.SetFloat("HorizontalSpeed", Mathf.Abs(horizontalInput));
        animator.SetFloat("VerticalSpeed", Mathf.Abs(verticalInput));
        
    }

    public void TakeDamage(int damage)
    {
        if (currentHealth > 0)
        {
            currentHealth -= damage;
            //Check if the player is dead
            if (currentHealth <= 0)
            {
                Die();
            }
        }
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
    // Trigger the "Die" animation if you have such an animation in your Animator
    if (animator != null)
    {
        animator.SetTrigger("Die");
    }
    
    // You can use an Animation event or a coroutine to delay the destruction
    // For example, using a coroutine:
    StartCoroutine(DestroyAfterAnimation());
}

// Coroutine to destroy the game object after the death animation
IEnumerator DestroyAfterAnimation()
{
    // Wait for the animation to finish (you may need to adjust the time accordingly)
    yield return new WaitForSeconds(1.0f); // Adjust the duration as needed

    // Destroy the game object
    Destroy(gameObject);
}
}