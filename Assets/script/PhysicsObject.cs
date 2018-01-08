using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//code url: https://unity3d.com/learn/tutorials/topics/2d-game-creation/horizontal-movement?playlist=17093

public class PhysicsObject : MonoBehaviour
{

    public float minGroundNormalY = .65f;
    public float gravityModifier = 1f; //this is var used to modify gravity, ex if we want half gravity then 0.5*gravityModifier

    protected Vector2 targetVelocity;
    protected bool grounded;
    protected Vector2 groundNormal;
    protected Rigidbody2D rb2d;
    protected Vector2 velocity;
    protected ContactFilter2D contactFilter;
    protected RaycastHit2D[] hitBuffer = new RaycastHit2D[16];
    protected List<RaycastHit2D> hitBufferList = new List<RaycastHit2D>(16);


    protected const float minMoveDistance = 0.001f; //this variable will be used to check if we are actually moving
    protected const float shellRadius = 0.01f; //this will be used to make sure we are not stuck in another collider

    void OnEnable()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        print("script start");
        //not checking collision against triggers
        contactFilter.useTriggers = false; 

        /*see edit->project setting->physics 2D, it will show layer collision matrix
         * collision is true if box is checked
         */
        contactFilter.SetLayerMask(Physics2D.GetLayerCollisionMask(gameObject.layer)); 
        contactFilter.useLayerMask = true;
    }

    void Update()
    {
        targetVelocity = Vector2.zero;
        ComputeVelocity();
    }

    protected virtual void ComputeVelocity()
    {

    }

    protected virtual void damage()
    {

    }

    /* we want to move object downward every fixed framerate frame (gravity), so we want to put 
     * gravity physics in FixedUpdate().
     */
    void FixedUpdate()
    {
        velocity += gravityModifier * Physics2D.gravity * Time.deltaTime;
        velocity.x = targetVelocity.x;

        grounded = false;

        Vector2 deltaPosition = velocity * Time.deltaTime;

        Vector2 moveAlongGround = new Vector2(groundNormal.y, -groundNormal.x);

        Vector2 move = moveAlongGround * deltaPosition.x;

        Movement(move, false);

        move = Vector2.up * deltaPosition.y;

        Movement(move, true);
    }

    //move is the direction we will be moving
    void Movement(Vector2 move, bool yMovement)
    {
        float distance = move.magnitude;

        /* if we are trying to move a distance less than minDistance,
         * then we do not have to constantly checking for collision when we are standing still
         */
        if (distance > minMoveDistance)
        {
            /* Cast will cast the attached collider2d shapes from the initial position to 
             * a specified direction for an optional distance, and check for overlapping.
             * So basically, in the next frame, will this box(s) collide with something else
             */
            int count = rb2d.Cast(move, contactFilter, hitBuffer, distance + shellRadius);
            hitBufferList.Clear();

            /* count is the number of hits, or indexes that actually has stuff in hitBuffer[]
             * we need to loop count number of times because we want indexes that has stuff.
             * Why not just use the array? Is it because it is global and has no easy way to clear?
             */
            for (int i = 0; i < count; i++)
            {
                hitBufferList.Add(hitBuffer[i]);
            }


            for (int i = 0; i < hitBufferList.Count; i++)
            {
                Vector2 currentNormal = hitBufferList[i].normal;
                if (currentNormal.y > minGroundNormalY)
                {
                    grounded = true;
                    if (yMovement)
                    {
                        groundNormal = currentNormal;
                        currentNormal.x = 0;
                    }
                }

                float projection = Vector2.Dot(velocity, currentNormal);
                if (projection < 0)
                {
                    velocity = velocity - projection * currentNormal;
                }

                float modifiedDistance = hitBufferList[i].distance - shellRadius;
                distance = modifiedDistance < distance ? modifiedDistance : distance;
            }


        }

        rb2d.position = rb2d.position + move.normalized * distance;
    }

}