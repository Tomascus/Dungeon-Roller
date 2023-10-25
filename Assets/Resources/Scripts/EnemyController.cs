using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    //For AI follow and damage I used tutorial from: https://www.youtube.com/watch?v=p7FnfMRQ6Ec&list=PL0GUZtUkX6t7zQEcvKtdc0NvjVuVcMe6U&index=4&ab_channel=GregDevStuff
    //Other functionalities were done by myself

    [Header("Enemy Settings")]
    public Animator animator;
    public float moveSpeed = 5f;
    public PlayerController playerController; //Reference the player controller
    public GameObject targetGameobject; //Target for enemy to chase
    SpriteRenderer character; //for flipping the sprite
    public EnemySpawner enemySpawner;
   
    
    

    [Header("Health System")]
    public int maxHealth = 100;
    public int currentHealth;
    [SerializeField] StatusBar hpBar;

    [Header("Damage System")]
    public int attackDamage = 1;
    public float attackSpeed = 0.5f;
    bool OnCooldown = false; //Cooldown for attacks

    private Rigidbody2D body;
    

    //Set current health of enemy to max health at the start and get Rigidbody Component for the enemies
    void Awake()
    {
        body = this.GetComponent<Rigidbody2D>();
        currentHealth = maxHealth; 
        character = this.GetComponent<SpriteRenderer>(); //getting sprite component
        enemySpawner = Object.FindFirstObjectByType<EnemySpawner>(); //getting enemySpawner
    }

    //Update is called once per frame
    void FixedUpdate()
    {
        //Following player when he is alive
        if (targetGameobject != null && playerController.IsPlayerAlive())
        {
            //gets the direction using players position and subtracting enemy  position and normalizes it for constant speed using unit vectors
            Vector3 direction = (targetGameobject.transform.position - transform.position).normalized;
            body.velocity = direction * moveSpeed; //sets the movement speed for following

            Flip(direction);
        
            //Calculate the x direction speed and start animation if the entity moves
            animator.SetFloat("Speed", Mathf.Abs(direction.x));
        } else 
        {
            body.velocity = Vector2.zero; //stops following when player dies by stopping velocity
            animator.SetFloat("Speed", 0); //Stops the animation - returns to idle
        }
    }

    public void TakeDamage(int damage)
    {
        if (currentHealth > 0)
        {   
            currentHealth -= damage;
            animator.SetTrigger("IsHit");
            //Check if the enemy is dead
            if (currentHealth <= 0 )
            {
                enemySpawner.EnemyDefeated(); //If the nemy is defeated, we remove 1 from total enemies in EnemySpawner
                Die();
            }
            hpBar.SetState(currentHealth, maxHealth);
        }
    }
   private void OnCollisionStay2D(Collision2D collision) 
   {
    if (collision.gameObject == targetGameobject)
    {
        Attack();
    }   
    }
   

   private void Attack()
   {
    if(!OnCooldown) //If there is no cooldown for attack, attack the entity
    {
    playerController.TakeDamage(attackDamage);
    animator.SetTrigger("IsAttacking");
    StartCoroutine(Cooldown()); //Starting cooldown for attack
    }
   }

   private IEnumerator Cooldown() //Cooldown coroutine for entities
   {
    OnCooldown = true; //Starts a cooldown
    yield return new WaitForSeconds(attackSpeed); //Set the cooldown time 
    OnCooldown = false; //Reset the cooldown
   }


    
    void Flip(Vector3 direction) //Flip method with direction value
    {

        character.flipX = direction.x < 0; //uses character sprite flip when the entity is moving to the left (if x is < 0)
        
        //Flip the characters sprite
        /*Vector3 currentScale = transform.localScale;
        currentScale.x *= -1;
        transform.localScale = currentScale;

        //Update the facing direction
        facingRight = !facingRight;*/
    }

    void Die()
{
    
        //Trigger the "Die" animation
        animator.SetTrigger("Die");
    
}

public void DestroyObject()
{
    //Destroy the game object immediately for animator
    Destroy(gameObject);
}

}
