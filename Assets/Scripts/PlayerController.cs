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
    public bool isTouchingTelephone;//Whether or not the player is touching a vessel.
    public bool isTouchingDoll;//Whether or not the player is touching the doll.

    private Vector3 moveDirection = Vector3.zero;
    private CharacterController controller; //I'm not liking the stock Unity CharacterController component. I want to change to movement using rigidbody at some point.

    // Double Jumping
    private int extraJumps;//Amount of double jumps available to the player. Resets when grounded.
    public int extraJumpsValue;//Maximum amount of double jumps available.

    // Wall Jumping
    private Vector3 wallPos;//Checks to see where the colliding wall is positioned.

    // Teleporting
    private Vector3 playerPosition;//Position of the player when using the Telephone ability
    private Vector3 vesselPosition;//Position of the paired Telephone when the player uses the Telephone ability.
    
    // Vessel Types
    public enum PlayerType
    {
        Ghost,
        Doll,
        Lamp,
        Telephone,
        Mouse
    }

    private GameObject collidingVessel;
    public GameObject possessedVessel;

    public PlayerType playerType;
    public Material ghostMesh; //Due to time constraints, we're changing colour for now rather than the mesh filter.
    public Material dollMesh;
    public Material telephoneMesh;

    public bool isGhost; //Redundant, but Oscar's KeyManager script relies on this. Currently don't have time to change.
    public bool isDoll; //Redundant, but Oscar's KeyManager script relies on this. Currently don't have time to change.

    // Start is called before the first frame update
    void Start()
    {
        //Store our controller
        controller = GetComponent<CharacterController>();
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Wall")
        {
            isTouchingWall = true;
            wallPos = other.ClosestPoint(this.transform.position);
        }

        if (other.tag == "Telephone" && isDoll == false)
        {
            isTouchingTelephone = true;
            collidingVessel = other.gameObject;
        }

        if (other.tag == "Doll" && isDoll == false)
        {
            isTouchingDoll = true;
            collidingVessel = other.gameObject;
        }

        //if (other.tag == "Player1" && isDoll == true || other.tag == "Player2" && isDoll == true) //I don't like this, but the project is due in 3 hours and I can't concentrate properly.
        //{
        //    ExitVessel();
            //TODO: Halt player movement temporarily
        //}
    }
    void OnTriggerExit(Collider other)
    {
        if(other.tag == "Wall")
        {
            isTouchingWall = false;
        }

        if (other.tag == "Telephone")
        {
            isTouchingTelephone = false;
        }
        
        if (other.tag == "Doll")
        {
            isTouchingDoll = false;
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

        //Vessel stuff
        if(Input.GetButtonDown("Special_P" + playerNumber))
        {
            switch (playerType)
            {
                case PlayerType.Ghost:
                    if (isTouchingTelephone == true)
                    {
                        gameObject.GetComponent<MeshRenderer>().material= telephoneMesh; //Replaces current mesh with the telephoneMesh
                        playerType = PlayerType.Telephone; //The PlayerType is now "Telephone"
                        possessedVessel = collidingVessel;
                        possessedVessel.SetActive(false); //Disables the colliding telephone. TODO: When re-enabled, move its position to next to the player.
                        Debug.Log("Possessing Telephone");
                    }

                    if (isTouchingDoll == true)
                    {
                        gameObject.GetComponent<MeshRenderer>().material= dollMesh; //Replaces current mesh with the dollMesh
                        playerType = PlayerType.Doll; //The PlayerType is now "Doll"
                        possessedVessel = collidingVessel;
                        possessedVessel.SetActive(false); //Disables the colliding doll. TODO: When re-enabled, move its position to next to the player.
                        isDoll = true; //Oscar's KeyManager script relies on the old isDoll boolean. Currently don't have time to change.
                        Debug.Log("Possessing Doll");
                    }

                    else
                    {
                        Debug.Log("Not near a vessel");
                        // Nothing should happen.
                        // Possibly include an indicator for the player so they are aware of this.
                    }
                    break;
                case PlayerType.Doll:
                    // We haven't designed a special ability for the doll. Can change later.
                    break;
                case PlayerType.Lamp:
                    // TODO: The lamp inverts gravity for the player.
                    break;
                case PlayerType.Telephone:
                    playerPosition = transform.position; //Gets players current position. Vessel will move here.
                    vesselPosition = GameObject.FindGameObjectWithTag("Telephone").transform.position; //Gets vessels current position. Player will move here

                    controller.enabled = false; //CharacterController messes with transform.position so I'm disabling it.
                    transform.position = vesselPosition; //Player moves to Vessel position.
                    GameObject.FindGameObjectWithTag("Telephone").transform.position = playerPosition; //Vessel moves to the tempPosition we got earlier.
                    controller.enabled = true; //Re-enable the CharacterController.
                    break;
                case PlayerType.Mouse:
                    // TODO: The mouse is capable of moving through walls.
                    break;
            }      
        }

        if(Input.GetButtonDown("K")) // Testing exiting vessels.
        {
            possessedVessel.SetActive(true);
            playerType = PlayerType.Ghost;
            gameObject.GetComponent<MeshRenderer>().material= ghostMesh; //Replaces current mesh with the ghostMesh
        }

        moveDirection.y += Physics.gravity.y * Time.deltaTime;
        controller.Move(moveDirection * Time.deltaTime);
    }

    //void VesselCheck() //If I had time and a bit more knowledge, this function would cover everything related to possessing, rather than me typing it out twice within the PlayerType.Ghost case.
    //{
        /*
        TODO:
        - Check what vessel the player is touching,
        - Change the player's mesh accordingly,
        - Hide the colliding vessel gameObject,
        - Assign a new PlayerType.
        */
    //}

    void ExitVessel()
    {
        //controller.enabled = false;
        possessedVessel.SetActive(true);
        playerType = PlayerType.Ghost;
        gameObject.GetComponent<MeshRenderer>().material= ghostMesh; //Replaces current mesh with the ghostMesh
        /*
        TODO:
        - Change player mesh to the ghost,
        - Assign "Ghost" as the new PlayerType,
        - Place the previously hidden vessel gameObject next to the player.
        */
    }
}