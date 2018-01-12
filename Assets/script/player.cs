using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//code url: https://unity3d.com/learn/tutorials/topics/2d-game-creation/horizontal-movement?playlist=17093
public class player : PhysicsObject {

    public float maxSpeed = 7;
    public float jumpTakeOffSpeed = 10;
    private bool facing = true; //true means character is facing right
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    public string objectName;
    public Collider2D attBox;
     // Use this for initialization
    void Awake () {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        objectName = spriteRenderer.name;
        
    }

    private void setAnimation(string name)
    {
        animator.SetBool("jump", Input.GetButtonDown("Jump"+name));
        animator.SetBool("jump_done", Input.GetButtonUp("Jump"+name));
        //animator.SetFloat("run", Mathf.Abs(velocity.x) / maxSpeed);

        animator.SetBool("run", Input.GetButton("Horizontal"+name));
        animator.SetBool("runbuttonup", Input.GetButtonUp("Horizontal"+name));

        animator.SetBool("crouch_down", Input.GetKey("down"));
        animator.SetBool("crouch_up", Input.GetKeyUp("down"));
    }

    private void compute(string name, ref Vector2 move)
    {
        move.x = Input.GetAxis("Horizontal"+name);
        //print("move.x is " + move.x);
        if (Input.GetButtonDown("Jump"+name) && grounded)
        {
            velocity.y = jumpTakeOffSpeed;
        }
        else if (Input.GetButtonUp("Jump"+name)) //canceling jump if jump button is let go
        {
            if (velocity.y > 0)
            {
                velocity.y = velocity.y * 0.5f;
            }
        }
    }

    // Update is called once per frame
    protected override void ComputeVelocity()
    {
        Vector2 move = Vector2.zero; ;

        //move.x = Input.GetAxis("Horizontal_p1");
        ////print("move.x is " + move.x);
        //if (Input.GetButtonDown("Jump_p1") && grounded)
        //{
        //    velocity.y = jumpTakeOffSpeed;
        //}
        //else if (Input.GetButtonUp("Jump_p1")) //canceling jump if jump button is let go
        //{
        //    if (velocity.y > 0)
        //    {
        //        velocity.y = velocity.y * 0.5f;
        //    }
        //}

        if (objectName == "player")
            compute("_p1", ref move);
        if (objectName == "player 2")
            compute("_p2", ref move);

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
            attBox.offset = new Vector2((float)-2.3, (float)0.2);
            print("attBox offset "+attBox.offset);
        }

        else if (move.x > 0.01f && facing == false)
        {
            facing = true;
            spriteRenderer.flipX = !spriteRenderer.flipX;
            attBox.offset = new Vector2((float)0.0, (float)0.2);
            print("attBox offset " + attBox.offset);
        }

        //if (!flipSprite)
        //{
        //    facing = flipSprite;
        //    spriteRenderer.flipX = !spriteRenderer.flipX;
        //}

        //print("move x and flip is " + move.x + " " + flipSprite);

        //setting up animation triggers in the animator component
        if(objectName == "player")
            setAnimation("_p1");
        if (objectName == "player 2")
            setAnimation("_p2");

        targetVelocity = move * maxSpeed;
    }
}
