using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    protected Animator anim;
    protected Rigidbody2D rb;
    public PlayerController controller;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        controller = GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetFloat("speed", Mathf.Abs(rb.velocity.x)); //update speed to Animator speed variable
        anim.SetFloat("velocity.y", rb.velocity.y);
        anim.SetBool("IsJump", controller.isJump);
        anim.SetBool("Ground", controller.isGround);


    }
} 
