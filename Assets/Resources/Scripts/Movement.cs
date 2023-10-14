using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f; //Controls for adjusting the move speed inside the game
    public float acceleration = 10f; //Controls for adjusting the acceleration for smoother movement inside the game

    private Rigidbody2D body;
    private Vector2 targetVelocity; 
    
    private void Awake() //Call when the scene is first initialized 
    {
        body = GetComponent<Rigidbody2D>();
        targetVelocity = Vector2.zero;
    }

    void Update()
    {
        //Getting inputs for horizontal and vertical movement
        float moveX = Input.GetAxis("Horizontal");
        float moveY = Input.GetAxis("Vertical");

        //Creating input velocity calculation for basic movement and then inputing it for final targetVelocity which adds acceleration calculation
        Vector2 inputVelocity = new Vector2(moveX, moveY) * speed;
        targetVelocity = Vector2.Lerp(targetVelocity, inputVelocity, Time.deltaTime * acceleration);

        //Applying smoother movement with acceleration to a rigidbody
        body.velocity = targetVelocity;
    }
}