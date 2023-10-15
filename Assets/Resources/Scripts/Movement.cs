using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f; //Controls for adjusting the move speed inside the game
    public float acceleration = 10f; //Controls for adjusting the acceleration for smoother movement inside the game
    public Animator animator;
    private Rigidbody2D body;
    private Vector2 targetVelocity; 
    private bool facingRight = true; //for fliping the character 
    
    private void Awake() //Call when the scene is first initialized 
    {
        body = GetComponent<Rigidbody2D>();
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
        animator.SetFloat("Speed", Mathf.Abs(horizontalInput));
        
    }

    //From school class
    private void Flip()
    {
        Vector3 currentScale = gameObject.transform.localScale;
        currentScale.x *= -1;
        gameObject.transform.localScale = currentScale;
        facingRight = !facingRight;
    }
}