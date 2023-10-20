using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    //For this script to create a simple AI that follows a player I followed tutorial: https://www.youtube.com/watch?v=4Wh22ynlLyk&ab_channel=PressStart
    //I added additional functionality such as animations, side switch myself

    [Header("Enemy Settings")]
    public Animator animator;
    public Transform player;
    public float moveSpeed = 5f;

    [Header("Health System")]
    public int maxHealth = 100;
    public int currentHealth;

    [Header("Damage System")]
    public int attackDamage = 10;
    public float attackSpeed = 1.0f;
    private float nextAttackTime = 0f;

    private Rigidbody2D body;
    private Vector2 movement;
    private bool facingRight = true; //for fliping the character 

    //Set current health of enemy to max health at the start and get Rigidbody Component for the enemies
    void Start()
    {
        body = this.GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
    }

    //Update is called once per frame
    void Update()
    {
        Vector3 direction = player.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        direction.Normalize();
        movement = direction;

        if ((movement.x > 0 && !facingRight) || (movement.x < 0 && facingRight))
        {
            Flip();
        }
        
    }

    private void FixedUpdate()
    {
        Move(movement);
        
    }

    void Move(Vector2 direction)
    {
        body.MovePosition((Vector2)transform.position + (direction * moveSpeed * Time.deltaTime));
    }

    public void TakeDamage(int damage)
    {
        if (currentHealth > 0)
        {
            currentHealth -= damage;
            // Check if the enemy is dead
            if (currentHealth <= 0)
            {
                Die();
            }
        }
    }

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
    if (animator != null)
    {
        //Trigger the "Die" animation
        animator.SetTrigger("Die");
    }
}

public void DestroyObject()
{
    //Destroy the game object immediately for animator
    Destroy(gameObject);
}

}
