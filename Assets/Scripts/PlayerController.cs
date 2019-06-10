using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public float jumpForce;
    private float moveInput;

    private Rigidbody rb;

    public SpriteRenderer spriteFlip; //We wiill need an alternate method once we start using 3D models. Joe.

    //The player can only jump whilst grounded. Add jumpable surfaces to the "Ground" tag in the inspector.
    private bool isTouchingGround = false;

    //Adds posibility of double jumping. 
    public int extraJumps;
    public int extraJumpsValue;

    //The player can walljump if WallCheck box collider collides with "Ground" or "Wall" tags.
    private bool isTouchingWall = false;

    //Adds possiblity of wall jumping.
    public int wallJumps;
    public int wallJumpsValue;

    //Essential jumping variables.
    public float jumpTimeCounter;
    public float jumpTime;
    public bool isJumping;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        //Rotating the sprite based on direction player is moving.
        if(moveInput < 0)
        {
            spriteFlip.flipX = true;
        }
        else if(moveInput > 0)
        {
            spriteFlip.flipX = false;
        }

        //Resets extraJumps and wallJumps value when thouching ground.
        if(isTouchingGround == true)
        {
            extraJumps = extraJumpsValue;
            wallJumps = wallJumpsValue;
        }
        if(Input.GetKeyDown(KeyCode.Space) && extraJumps > 0)
        {
        isJumping = true;
        jumpTimeCounter = jumpTime;            
            CharacterJump();
            extraJumps--;
        }
        if(Input.GetKeyDown(KeyCode.Space) && wallJumps > 0 && isTouchingWall == true)
        {
        isJumping = true;
        jumpTimeCounter = jumpTime;            
            CharacterJump();
            wallJumps--;
        }
        else if(Input.GetKeyDown(KeyCode.Space) && extraJumps == 0 && isTouchingGround == true)
        {
        isJumping = true;
        jumpTimeCounter = jumpTime;            
            CharacterJump();
        }

        //Holding Jump allows for higher jumps
        if (Input.GetKey(KeyCode.Space) && isJumping == true)
        {
            if (jumpTimeCounter > 0)
            {
                rb.velocity = Vector2.up * jumpForce;
                jumpTimeCounter -= Time.deltaTime;
            }
            else
            {
                isJumping = false;
            }
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            isJumping = false;
        }
    }
 
    //Checks if the player is touching the ground.
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Ground")
        {
            isTouchingGround = true;
        }
        else if(other.tag == "Wall")
        {
            isTouchingWall = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Ground")
        {
            isTouchingGround = false;
        }
        else if(other.tag == "Wall")
        {
            isTouchingWall = false;
        }
    }
    
    // Jumping stuff
    void CharacterJump()
    {
        rb.velocity = Vector2.up * jumpForce;
    }

    void FixedUpdate()
    {
        //Movement stuff
        moveInput = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);
    }
}
