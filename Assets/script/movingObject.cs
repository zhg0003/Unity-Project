using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//reference: https://www.youtube.com/watch?v=fURWEzpNPL8&index=6&list=PLX2vGYjWbI0SKsNH5Rkpxvxr1dPE0Lw8F

public abstract class movingObject : MonoBehaviour
{

    public float moveTime = 0.1f; //time it takes for object to move
    public LayerMask blockingLayer; //this will be used check for collision

    private BoxCollider2D box; //this will store the reference to the boxCollider 2D component of the game object
    private Rigidbody2D body; //this will store the reference to the rigidbody component of the game object

	// Use this for initialization
	protected virtual void Start ()
    {
        box = GetComponent<BoxCollider2D>();
        body = GetComponent<Rigidbody2D>();
	}
	
    protected bool Move (int xDir, int yDir, out RaycastHit2D hit) //out cause arguement to be pass by reference, simliar to & in C++
    {
        Vector2 start = transform.position; //transform is the position, rotation and scale of an object

        /*
         * we can calculate the end vector by adding our starting 
         * vector (start) to the new vector created using xDir yDir
         * */

        Vector2 end = start + new Vector2(xDir, yDir);
        box.enabled = false;  //make sure we do not hit our own collider when we do ray casting

        /*Linecast cast a line between start and end, return true if there is collider in between
         *third arguement is used to selectively ignore colliders when casting a ray
         * */
        hit = Physics2D.Linecast(start, end, blockingLayer);
        box.enabled = true;

        if(hit.transform == null) //getting null means we can move the object
        {
            StartCoroutine(SmoothMovement(end));
            return true;
        }
        return false;
    }

    protected IEnumerator SmoothMovement (Vector3 end) //end is the position we are trying to move to
    {
        float remainDistance = (transform.position - end).sqrMagnitude;
        while(remainDistance > float.Epsilon)
        {
            /*MoveTowards() moves a point in a straight line towards target point, 
             * the point will be moved by  (1 / moveTime * Time.deltaTime) units
             * */
            Vector3 newPosition = Vector3.MoveTowards(body.position, end, 1 / moveTime * Time.deltaTime);
            body.MovePosition(newPosition);
            remainDistance = (transform.position - end).sqrMagnitude;
            yield return null; //wait for a frame to pass then we evaluate the condition of the loop
        }
    }
	// Update is called once per frame
	void Update () {
		
	}

    /*T is the generic type that player is expected to encounter when blocked
     * in player case, it will be a wall
     * */
    protected virtual void AttemptMove<T> (int xDir, int yDir) where T: Component 
    {
        RaycastHit2D hit;
        bool canMove = Move(xDir, yDir, out hit);

        if (hit.transform == null)
            return;

        T hitComponent = hit.transform.GetComponent<T>();
        if (!canMove && hitComponent != null)
            OnCantMove(hitComponent);
    }

    protected abstract void OnCantMove<T>(T component) where T : Component;
}
