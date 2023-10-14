using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementTest : MonoBehaviour
{
    public float speed = 5f;
    private Rigidbody2D body;
    
    private void Awake() 
    {
        body = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        body.velocity = new Vector2(speed, speed);
    }
    // Update is called once per frame
    
}
