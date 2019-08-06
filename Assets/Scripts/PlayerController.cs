using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{    
    public int playerNumber;//Assign Player Number to P1 and P2 in the inspector. Player 1 = 1, Player 2 = 2 etc.
    
    public float speed = 6.0f;//Speed the player moves.
    public float jumpForce = 15f;//Amount of force added when the player jumps.
    public float fallMultiplier = 2.1f;//Faster falling after jumping.
    public float lowJumpMultiplier = 1.9f;//Faster falling
    public bool isTouchingWall;//Whether or not the player is touching a wall.
    public bool isTouchingVessel;//Whether or not the player is touching a possessable vessel.

    private Vector3 moveDirection = Vector3.zero;
    private CharacterController controller;

    // Double Jumping
    private int extraJumps;//Amount of double jumps available to the player. Resets when grounded.
    public int extraJumpsValue;//Maximum amount of double jumps available.

    // Wall Jumping
    private Vector3 wallPos;//Checks to see where the colliding wall is positioned.

    // Teleporting
    private Vector3 playerPosition;//Position of the player when using the Telephone ability
    private Vector3 vesselPosition;//Position of the paired Telephone when the player uses the Telephone ability.
    
    // Vessel Types - Probably better to do an array or enums. Doing this for now. Public booleans just for sake of testing, change to private later.
    public bool isGhost;
    public bool isDoll;
    public bool isLamp;
    public bool isTelephone;
    public bool isMouse;

    // Start is called before the first frame update
    void Start()
    {
        //Store our controller
        controller = GetComponent<CharacterController>();
    }

    //Checks if the player is touching the wall or vessel.
    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Wall")
        {
            isTouchingWall = true;
            wallPos = other.ClosestPoint(this.transform.position);
        }

        if (other.tag == "Vessel")
        {
            isTouchingVessel = true;
        }
    }
    void OnTriggerExit(Collider other)
    {
        if(other.tag == "Wall")
        {
            isTouchingWall = false;
        }

        if (other.tag == "Vessel")
        {
            isTouchingVessel = false;
        }
    }    

    // Update is called once per frame
    void Update()
    {
        if (controller.isGrounded)
        {
            extraJumps = extraJumpsValue;

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
            if(Input.GetButtonDown("Jump_P" + playerNumber) && !isTouchingWall && extraJumps > 0)
            {
                moveDirection.y = jumpForce;
                extraJumps--;
            }

            //Wall Jumping
            if(Input.GetButtonDown("Jump_P" + playerNumber) && isTouchingWall)
            {
                moveDirection.x = Mathf.Sign(transform.position.x - wallPos.x) * 5; //Bounces player away from the colliding wall.
                moveDirection.y = jumpForce;
            }
        }
        
        //Better Jumps. This allows jumping to be less "floaty".
        if (controller.velocity.y <= 0)
        {
            //Increases gravity while falling.
            moveDirection += Vector3.up * Physics.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }
        else if (controller.velocity.y > 0 && !Input.GetButton ("Jump_P" + playerNumber))
        {
            //Shorter jumps by only tapping Jump button.
            moveDirection += Vector3.up * Physics.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        }

        //Vessel check
        if(isTouchingVessel)
        {

        }

        //Vessel stuff
        if(Input.GetButtonDown("Special_P" + playerNumber))
        {
            if(isGhost && isTouchingVessel)
            {
                
            }

            if(isDoll)
            {
                //Do shit
            }

            if(isLamp)
            {
                //Do shit
            }

            if(isTelephone)
            {
                playerPosition = transform.position; //Gets players current position. Vessel will move here.
                vesselPosition = GameObject.Find("Telephone").transform.position; //Gets vessels current position. Player will move here

                controller.enabled = false; //CharacterController messes with transform.position so I'm disabling it.
                transform.position = vesselPosition; //Player moves to Vessel position.
                GameObject.Find("Telephone").transform.position = playerPosition; //Vessel moves to the tempPosition we got earlier.
                controller.enabled = true; //Re-enable the CharacterController.
            }

            if(isMouse)
            {
                //Do shit
            }            
        }

        moveDirection.y += Physics.gravity.y * Time.deltaTime;
        controller.Move(moveDirection * Time.deltaTime);
    }
}
