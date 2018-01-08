using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : PhysicsObject {

    public float maxSpeed = 7;
    public float jumpTakeOffSpeed = 10;
    private bool facing = true; //true means character is facing right
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    private int chealth;
    private int maxhealth;

     // Use this for initialization
    void Awake () {
        chealth = 100;
        maxhealth = 100;
        print("starting player");
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    protected override void ComputeVelocity()
    {
        Vector2 move = Vector2.zero;

        move.x = Input.GetAxis("Horizontal");
        //print("move.x is " + move.x);
        if (Input.GetButtonDown("Jump") && grounded)
        {
            velocity.y = jumpTakeOffSpeed;
        }
        else if (Input.GetButtonUp("Jump")) //canceling jump if jump button is let go
        {
            if (velocity.y > 0)
            {
                velocity.y = velocity.y * 0.5f;
            }
        }

        //bool flipSprite = (spriteRenderer.flipX ? (move.x > 0.01f) : (move.x < 0.01f));
        // bool flipSprite = false;

        //facing movex result expected
        //0      0     true   false 
        //0      1     false  true
        //1      0     false  true
        //1      1     true   false 

        if (move.x < 0 && facing == true)
        {
            facing = false;
            spriteRenderer.flipX = !spriteRenderer.flipX;
        }

        else if (move.x > 0.01f && facing == false)
        {
            facing = true;
            spriteRenderer.flipX = !spriteRenderer.flipX;
        }

        //if (!flipSprite)
        //{
        //    facing = flipSprite;
        //    spriteRenderer.flipX = !spriteRenderer.flipX;
        //}

        //print("move x and flip is " + move.x + " " + flipSprite);

        //setting up animation triggers in the animator component
        animator.SetBool("jump", Input.GetButtonDown("Jump"));
        animator.SetBool("jump_done", Input.GetButtonUp("Jump"));
        //animator.SetFloat("run", Mathf.Abs(velocity.x) / maxSpeed);

        animator.SetBool("run", Input.GetButton("Horizontal"));
        animator.SetBool("runbuttonup", Input.GetButtonUp("Horizontal"));

        animator.SetBool("crouch_down", Input.GetKey("down"));
        animator.SetBool("crouch_up", Input.GetKeyUp("down"));

        animator.SetBool("attack", Input.GetKeyDown("s"));

        targetVelocity = move * maxSpeed;
    }
}
