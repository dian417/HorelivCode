using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scene1 : PlayerController
{
    public Leaf leaf;
    public NPC_SeaM seam;
    

    public bool isShift;
    public bool canShift;
    public bool ShiftGround;
    public bool canBubble;
    public bool isBubble;
    public float finishedTime;

    public override void Update()
    {
        CheckInput();
        if (canBubble && seam.startTime != 0)
        {
            Bubble();
        }

        Flow();
    }

    public override void FixedUpdate()//all physical movements
    {
        PhysicsCheck();
        Movement();
        Jump();
        Shift();
        Bubble();
        deathCheck();
    }

    public override void CheckInput()
    {
        //jump
        if (Input.GetButtonDown("Jump") && isGround)
        {
            canJump = true;
        }

        //player swinging using the vine
        if (Input.GetButtonDown("catch") && leaf.isTriggered && ShiftGround)
        {
            canShift = true;
        }

        //player getting bubble power from NPC Seam
        if (Input.GetKeyDown(KeyCode.R) && seam.isTriggered)
        {
            canBubble = true;

        }

        //after getting bubble, flow into the air
        if (Input.GetKeyDown(KeyCode.R) && isBubble)
        {
            isBubble = false;
            rb.gravityScale = 8;
        }

    }

    /* Method Name: Shift
     * Description: Swing using vine on the ceiling
     */
    void Shift()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        if (canShift)
        {
            isShift = true;
            oriSpeed = rb.velocity.x;
            rb.velocity = new Vector2(horizontalInput * shiftForce, rb.velocity.y + shiftForce / 4f);
            canShift = false;
        }

    }

    void Bubble()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");

        if (canBubble)
        {
            finishedTime = seam.startTime + 2f;
            if (Time.time >= finishedTime)
            {
                isBubble = true;
                rb.gravityScale = 0;
                rb.velocity = new Vector2(horizontalInput * speed, flowForce);
                canBubble = false;
            }

        }

    }

    /* Method Name: Flow
     * Description: Seam's special power, using bubble to lift player into the air.
     */
    void Flow()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        if (isBubble)
        {
            rb.velocity = new Vector2(horizontalInput * speed, flowForce);
        }
    }


    public override void deathCheck()
    {
        //first open gap with vine
        if (transform.position.y <= (-10f) && transform.position.x <= (17f))
        {
            transform.position = checkpoint;
            FollowCamera.transform.position = cameraPos;
        }

        //second open gap on the lower level
        if (transform.position.y <= -47f && checkpoint.y >= -10f)
        {
            //update checkpoint to level2
            checkpoint = transform.position;
            cameraPos = FollowCamera.transform.position;
        }
        //out of the gameplay boundary
        if (transform.position.y <= (-51f))
        {
            transform.position = checkpoint;
            FollowCamera.transform.position = cameraPos;
        }
    }

    public override void PhysicsCheck()
    {
        isGround = Physics2D.OverlapCircle(groundCheck.position, checkRadius, groundLayer);
        if (isGround)
        {
            isJump = false;
            isShift = false;
            ShiftGround = true;
        }

    }

}
s