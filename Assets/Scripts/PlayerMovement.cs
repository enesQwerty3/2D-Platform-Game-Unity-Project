using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerMovement : MonoBehaviour
{
    private Vector2 movement = Vector2.zero;
    private Rigidbody2D rb;
    [SerializeField] private Vector2 SpawnPos = new Vector2 (1.5f, 0.5f); 
    [Range(2f, 300f)]
    [SerializeField] private float movementSpeed = 150f;
    [Range(2f, 100f)]
    [SerializeField] private float jumpPower = 50f;
    [Range(0f, 10f)]
    [SerializeField] private float jumpCooldown = 3f;
    private bool jumpKeyPressed = false;
    private float nextJumpTime = 0;
    private int jumpCount = 0;
    private float lastJumpTime;
    private bool jumpOnCooldown = false;
    private bool startCoolDown = false;
    //[SerializeField] LayerMask groundLayerMask;
    
     // Start is called before the first frame update
    void Start()
    {
        //Debug.Log(gameObject.name);
        rb = GetComponent<Rigidbody2D>();
        transform.position = SpawnPos;
    }

    // Update is called once per frame  
    void Update()
    {
        //EnableInput()
        movement.x = Input.GetAxisRaw("Horizontal");
        if(Input.GetKeyDown("space"))
        {
            jumpKeyPressed = true;
        }
        //Debug.Log(transform.position);        
    }

    void FixedUpdate() //physics based actions  
    {
        MovePlayer();
        JumpPlayer();
    }

    /*bool IsGrounded()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(rb.position, 0.2f, groundLayerMask); 
        if (colliders != null)
        {
            foreach (var collider in colliders)
            {
                Debug.Log("xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx");
            }
            return true;
        }

        else
        {
            return false;
        }    
    }*/

    //private Vector3 lastPosition = Vector3.zero;
    void MovePlayer()
    {
        //float speed = 0;
        
        if(movement.x != 0f)
        {
            //speed = Vector3.Distance(lastPosition, transform.position) / Time.deltaTime;
            //lastPosition = transform.position;
            //Debug.Log(speed);
            
            Vector3 velocity = rb.velocity;  //constant rigidbody speed
            velocity.x = movement.x * movementSpeed * Time.deltaTime;
            rb.velocity = velocity;

            /*
            Vector3 velocity = rb.velocity;  //multiplying speed of rigidbody by movementSpeed value while holding arrow keys.
            velocity.x = rb.velocity.x + movement.x * movementSpeed * Time.deltaTime;
            rb.velocity = velocity;
            */

            //rb.velocity = movement * movementSpeed * Time.deltaTime; //when jumped affects horizontal speed of player not usefull for player movement
            //transform.position = transform.position + movement * movementSpeed * Timte.deltaTime; //changing position via transform.posiion

        } 
        
    }

    void JumpPlayer()
    {
        if(jumpKeyPressed && !jumpOnCooldown)
        {  
            float resetJumpCountSeconds = 1f;
            //rb.velocity = Vector2.up * jumpPower;
            if(Time.time < lastJumpTime + resetJumpCountSeconds)  //don't reset jump if player press jump key with out waiting reset time
            {
                jumpCount++;
            }

            else if(lastJumpTime == 0)    //set start point for lastJumpTime
            {
                jumpCount++;
            }

            else if(Time.time > lastJumpTime + resetJumpCountSeconds)   //reset double jump
            {
                jumpCount = 1;
            }    
                
            Debug.Log("Jump count: " + jumpCount);

            if(jumpCount <= 2)
            {
                rb.AddForce(transform.up * jumpPower, ForceMode2D.Impulse);
                lastJumpTime = Time.time;
                if(jumpCount == 2)
                    startCoolDown = true;
            }    
            jumpKeyPressed = false;
        }

        else if(jumpCount == 2 && !jumpKeyPressed && startCoolDown)    //run if jump key pressed 2 times and calculate cooldown time
        {
            //Debug.Log("Cooldown time enabled at:" + Time.time);
            nextJumpTime = Time.time + jumpCooldown;
            startCoolDown=false;
            jumpOnCooldown = true;
        }

        else if(jumpOnCooldown)                   //disable jump and wait for cooldown time
        {
            Debug.Log("Cooldown time enabled at:" + Time.time);
            Debug.Log("Next Jump Time:" + nextJumpTime);
            Debug.Log("Current time: " + Time.time);

            if(Time.time >= nextJumpTime)
            {
                nextJumpTime = 0;
                jumpOnCooldown = false;
            }
        }
        jumpKeyPressed = false;
    }
}
