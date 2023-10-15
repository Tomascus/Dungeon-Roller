using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    //For this script to create a simple AI that follows a player I followed tutorial: https://www.youtube.com/watch?v=4Wh22ynlLyk&ab_channel=PressStart
    //I added additional functionality such as animations, side switch myself
    public Animator animator;
    public Transform player;
    public float moveSpeed = 5f;
    private Rigidbody2D body;
    private Vector2 movement;
    // Start is called before the first frame update
    void Start()
    {
        body = this.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = player.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        body.rotation = angle;
        direction.Normalize();
        movement = direction;
        
    }

    private void FixedUpdate()
    {
        moveCharacter(movement);
    }

    void moveCharacter(Vector2 direction)
    {
        body.MovePosition((Vector2)transform.position + (direction * moveSpeed * Time.deltaTime));
    }
}
