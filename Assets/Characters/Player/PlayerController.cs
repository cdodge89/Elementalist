using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
    public float speed;
    public float jumpVal;
    private Rigidbody rb;
    private float distanceToGround;
    private Collider col;
    private Animator animator;

    private float y_rot;


	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
        col = GetComponent<Collider>();
        animator = GetComponent<Animator>();
        y_rot = rb.rotation.y;
        distanceToGround = col.bounds.extents.y;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        int up = Input.GetKey(KeyCode.UpArrow) ? 1: 0;
        int down = Input.GetKey(KeyCode.DownArrow) ? 1 : 0;
        int left = Input.GetKey(KeyCode.LeftArrow) ? 1 : 0;
        int right = Input.GetKey(KeyCode.RightArrow) ? 1 : 0;
        bool jump = Input.GetKey(KeyCode.Space);
        int moveHorizontal = right - left;
        int moveVertical = up - down;

        //Debug.Log(Mathf.Abs(rb.velocity.y));

        if (jump && isGrounded(distanceToGround))
        {
            Debug.Log("jumped");

            jumpFunc(jumpVal);
        }

        if(moveHorizontal != 0 || moveVertical != 0){
            y_rot = Mathf.Acos(moveVertical) * Mathf.Rad2Deg;
        } 
        
        

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        movement = Vector3.ClampMagnitude(movement*speed, speed);
        //Debug.Log(movement);

        //

        if(rb.velocity.sqrMagnitude < speed * speed) {
            animator.SetBool("isMoving",true);
            rb.AddForce(movement);
            
        }

        if(moveHorizontal != 0 && moveVertical != 0){
            rb.AddForce(-rb.velocity.x, 0, 0);
            rb.AddForce(0, 0, -rb.velocity.z);
            rb.AddForce(movement);
        }

        if(moveHorizontal == 0)
        {
            rb.AddForce(-rb.velocity.x, 0, 0);
            
        }
        if(moveVertical == 0)
        {
            rb.AddForce(0, 0, -rb.velocity.z);
            
        }
        if(moveHorizontal == 0 && moveVertical == 0){
            animator.SetBool("isMoving",false);
            //y_rot = rb.transform.rotation.y * Mathf.Rad2Deg;
           // Debug.Log("y_rot");
           // Debug.Log(y_rot);
        }
        // Debug.Log("y_rot");
        // Debug.Log(y_rot);
        // Debug.Log("rb rot");
        // Debug.Log(rb.rotation.y);
        rb.transform.rotation = Quaternion.Euler(-90f, y_rot, 0f);
    }

    void jumpFunc(float jumpVal)
    {
        animator.SetTrigger("jumpPressed");
        rb.AddForce(0, jumpVal, 0);
    }

    bool isGrounded(float distanceToGround)
    {
        return Physics.Raycast(transform.position, -Vector3.up, distanceToGround + 0.1f);
    }
}
