using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Rigidbody2D body;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //The bullet collided with an enemy, destroy it and take damage
        if (collision.gameObject.TryGetComponent<EnemyController>(out EnemyController enemyComponent))
        {
            enemyComponent.TakeDamage(10);
        }
        
        Destroy(gameObject);
    }
    
}
