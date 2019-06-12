using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //TODO: Use FixedUpdate for physics related code. IMPORTANT.
    //TODO: We should detect the inputs in Update and handle them in FixedUpdate.

    //TODO: Stop player from hanging in the air when hitting the ceiling mid-jump.
    
    //Assign Player Number to P1 and P2 in the inspector.
    public int playerNumber;
    
    public float speed = 6.0f;//Speed the player moves.
    public float jumpForce = 13.5f;//Amount of force added when the player jumps.
    public float fallMultiplier = 2.5f;//Faster falling after jumping.
    public float lowJumpMultiplier = 2f;//Faster falling
    public bool isTouchingWall;//Whether or not the player is touching a wall.

    private Vector3 moveDirection = Vector3.zero;
    private CharacterController controller;

    // Double Jumping
    public bool canDoubleJump;//Whether or not the player can double jump.
    private int extraJumps;//Amount of double jumps available to the player. Resets when grounded.
    public int extraJumpsValue;//Maximum amount of double jumps available.

    // Wall Jumping
    public bool canWallJump;//Whether or not the player can wall jump.
    private int wallJumps;//Amount of wall jumps available to the player. Resets when grounded.
    public int wallJumpsValue;//Maximum amount of wall jumps available.
    private Vector3 wallPos;//Checks to see where the colliding wall is positioned.

    // Start is called before the first frame update
    void Start()
    {
        //Store our controller
        controller = GetComponent<CharacterController>();
    }

    //Checks if the player is touching the wall.
    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Wall")
        {
            isTouchingWall = true;
            wallPos = other.ClosestPoint(this.transform.position);
        }
    }
    void OnTriggerExit(Collider other)
    {
        if(other.tag == "Wall")
        {
            isTouchingWall = false;
        }
    }    

    // Update is called once per frame
    void Update()
    {
        if (controller.isGrounded)
        {
            extraJumps = extraJumpsValue;
            wallJumps = wallJumpsValue;            

            moveDirection = new Vector3(Input.GetAxis("Horizontal_P" + playerNumber), 0, 0);
            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection *= speed;

            if(Input.GetButtonDown("Jump_P" + playerNumber))
            {
                moveDirection.y = jumpForce;
            }
        }

        if (!controller.isGrounded)
        {
            //Double Jumping
            if(Input.GetButtonDown("Jump_P" + playerNumber) && canDoubleJump && !isTouchingWall && extraJumps > 0)
            {
                moveDirection.y = jumpForce;
                extraJumps--;
            }

            //Wall Jumping
            if(Input.GetButtonDown("Jump_P" + playerNumber) && canWallJump && isTouchingWall && wallJumps > 0)
            {
                moveDirection.x = Mathf.Sign(transform.position.x - wallPos.x) * wallJumps; //Bounces player away from the colliding wall.
                moveDirection.y = jumpForce;
                wallJumps--;
            }
        }
        
        //Better Jumps. This allows jumping to be less "floaty".
        if (controller.velocity.y < 0)
        {
            //Increases gravity while falling.
            moveDirection += Vector3.up * Physics.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }
        else if (controller.velocity.y > 0 && !Input.GetButton ("Jump_P" + playerNumber))
        {
            //Shorter jumps by only tapping Jump button.
            moveDirection += Vector3.up * Physics.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        }

        moveDirection.y += Physics.gravity.y * Time.deltaTime;
        controller.Move(moveDirection * Time.deltaTime);
    }

    void FixedUpdate()
    {
        //TODO: Place all physics related code here. IMPORTANT.
    }
}