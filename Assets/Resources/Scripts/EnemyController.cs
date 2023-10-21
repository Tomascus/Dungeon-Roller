using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    //For AI follow and damage I used tutorial from: https://www.youtube.com/watch?v=p7FnfMRQ6Ec&list=PL0GUZtUkX6t7zQEcvKtdc0NvjVuVcMe6U&index=4&ab_channel=GregDevStuff
    //Other functionalities were done by myself

    [Header("Enemy Settings")]
    public Animator animator;
    public Transform player; //Target for the enemy to chase
    public float moveSpeed = 5f;
    public PlayerController playerController;
    GameObject targetGameobject;
    

    [Header("Health System")]
    public int maxHealth = 100;
    public int currentHealth;

    [Header("Damage System")]
    public int attackDamage = 1;
    public float attackSpeed = 1.0f;

    private Rigidbody2D body;
    private Vector2 movement;
    private bool facingRight = true; //for fliping the character 

    //Set current health of enemy to max health at the start and get Rigidbody Component for the enemies
    void Awake()
    {
        body = this.GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
        targetGameobject = player.gameObject;
    }

    //Update is called once per frame
    void FixedUpdate()
    {
        Vector3 direction = (player.position - transform.position).normalized;
        body.velocity = direction * moveSpeed;

        if ((direction.x > 0 && !facingRight) || (direction.x < 0 && facingRight))
        {
            Flip();
        }
        
        //Calculate the x direction speed and start animation if the entity moves
        animator.SetFloat("Speed", Mathf.Abs(direction.x));
    }

    public void TakeDamage(int damage)
    {
        if (currentHealth > 0)
        {   
            animator.SetBool("IsHit", true);
            currentHealth -= damage;
            animator.SetBool("IsHit", false);
            //Check if the enemy is dead
            if (currentHealth <= 0)
            {
                Die();
            }
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
    playerController.TakeDamage(attackDamage);
   }


    //From Naosse's class
    void Flip()
    {
        //Flip the characters sprite
        Vector3 currentScale = transform.localScale;
        currentScale.x *= -1;
        transform.localScale = currentScale;

        //Update the facing direction
        facingRight = !facingRight;
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
