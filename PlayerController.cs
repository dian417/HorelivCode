using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D rb;

    public float speed;
    public float jumpForce;
    public float shiftForce;
    public float flowForce;
    
    protected Vector3 cameraPos;
    protected float oriSpeed;
    protected Vector3 checkpoint;

    public GameObject FollowCamera;



    [Header("Ground Check")] //Unity UI
    public Transform groundCheck;
    public float checkRadius;
    public LayerMask groundLayer;
    

    [Header("States Check")]
    public bool isGround;
    public bool isJump;
    public bool canJump;



    // Start is called before the first frame update
    protected virtual void Start()
    {
        checkpoint = transform.position;
        cameraPos = FollowCamera.transform.position;
    }

    // Update is called once per frame
    public virtual void Update()
    {
        
        CheckInput();

    }

    public virtual void FixedUpdate()//all physical movements
    {
        PhysicsCheck();
        Movement();
        Jump();
    }

    
    public virtual void CheckInput()
    {
        if (Input.GetButtonDown("Jump") && isGround)
        {
            canJump = true;
        }


    }

    public void Movement()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal"); 

        rb.velocity = new Vector2(horizontalInput * speed, rb.velocity.y);//perform movement

        if (horizontalInput != 0)//if moving
        {
            //changing direction according input
            transform.localScale = new Vector3(horizontalInput, 1, 1);
        }

    }

    public void Jump()
    {
        if (canJump)
        {
            isJump = true;

            //Force to go up
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            rb.gravityScale = 9;

            canJump = false;
        }
    }

   

    public virtual void PhysicsCheck()
    {
        //circle where detect if player is on the ground
        isGround = Physics2D.OverlapCircle(groundCheck.position, checkRadius, groundLayer);
        if (isGround)
        {
            isJump = false;
            rb.gravityScale = 3;
        }
            
    }

    /*public virtual void deathCheck()
    {

    }*/

    public void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(groundCheck.position, checkRadius);
    }
}
