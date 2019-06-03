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

    //The player can only jump whilst grounded. Add jumpable surfaces to the "Ground" layer in the inspector.
    private bool isTouchingGround;
    public Transform groundCheckPoint;
    public float groundCheckRadius;
    public LayerMask groundLayer;

    //Adds posibility of double jumping. 
    private int extraJumps;
    public int extraJumpsValue;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        extraJumps = extraJumpsValue;
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

        //Checks to see if double jumping is possible.
        isTouchingGround = Physics2D.OverlapCircle(groundCheckPoint.position, groundCheckRadius, groundLayer);
        if(isTouchingGround == true)
        {
            extraJumps = extraJumpsValue;
        }
        if(Input.GetKeyDown(KeyCode.Space) && extraJumps > 0)
        {
            rb.velocity = Vector2.up * jumpForce;
            extraJumps--;
        }
        else if(Input.GetKeyDown(KeyCode.Space) && extraJumps == 0 && isTouchingGround == true)
        {
            rb.velocity = Vector2.up * jumpForce;
        }
    }

    void FixedUpdate()
    {
        //Movement stuff
        moveInput = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);
    }
}
