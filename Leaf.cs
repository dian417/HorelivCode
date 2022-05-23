using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Leaf : MonoBehaviour
{
    public bool isTriggered;
    public bool isShift;
    public Animator anim;
    private float startTime;


    // Start is called before the first frame update
    void Start()
    {
        isTriggered = false;
        isShift = false;
        anim.Play("Leaf");
    }

    // Update is called once per frame
    void Update()
    {
        Shift();
        if (Time.time > startTime + 3f)//if finished play, stop and trans back to stable mode.
        {
            anim.Play("Leaf");
            isShift = false;
            transform.localScale = new Vector3(1, 1, 1);
        }
   
    }

    void Shift()
    {
        if(Input.GetButtonDown("catch") && isTriggered)
        {
            isShift = true;

            float horizontalInput = Input.GetAxisRaw("Horizontal"); //get horizontal direction as -1~1

            if (horizontalInput != 0)//if player is inputing, not idle.
            {
                transform.localScale = new Vector3(horizontalInput, 1, 1);
            }

            anim.Play("shift");
            startTime = Time.time;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        isTriggered = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        isTriggered = false;
    }
}
