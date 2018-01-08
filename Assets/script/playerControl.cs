using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//code url: https://unity3d.com/learn/tutorials/topics/2d-game-creation/horizontal-movement?playlist=17093
public class PlayerPlatformerController : PhysicsObject
{

    public float maxSpeed = 7;
    public float jumpTakeOffSpeed = 7;
    private SpriteRenderer spriteRenderer;
    private Animator animator;

    // Use this for initialization

    void OnEnable()
    {
        print("script active");
    }

    //void Awake()
    //{
    //    print("in player awake");
    //    spriteRenderer = GetComponent<SpriteRenderer>();
    //    animator = GetComponent<Animator>();
    //}

    void Start()
    {
        print("starting player");
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }
    protected override void ComputeVelocity()
    {
        print("computing velocity");
        Vector2 move = Vector2.zero;

        move.x = Input.GetAxis("Horizontal");

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

        bool flipSprite = (spriteRenderer.flipX ? (move.x > 0.01f) : (move.x < 0.01f));
        print("flipSprite is " + flipSprite);
        if (flipSprite)
        {
            spriteRenderer.flipX = !spriteRenderer.flipX;
        }

        animator.SetBool("grounded", grounded);
        animator.SetFloat("velocityX", Mathf.Abs(velocity.x) / maxSpeed);

        targetVelocity = move * maxSpeed;
    }
}