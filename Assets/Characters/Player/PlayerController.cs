using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
    public float speed;
    public float jumpVal;
    private Rigidbody rb;
    private float distanceToGround;
    private Collider col;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
        col = GetComponent<Collider>();
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

        Debug.Log(Mathf.Abs(rb.velocity.y));

        if (jump && isGrounded(distanceToGround))
        {
            Debug.Log("jumped");

            jumpFunc(jumpVal);
        }

       


        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

        //

        if(rb.velocity.sqrMagnitude < speed * speed) {
            rb.AddForce(movement * speed);
        }

        if(moveHorizontal == 0)
        {
            rb.AddForce(-rb.velocity.x, 0, 0);
        }
        if(moveVertical == 0)
        {
            rb.AddForce(0, 0, -rb.velocity.z);
        }
        
    }

    void jumpFunc(float jumpVal)
    {
        rb.AddForce(0, jumpVal, 0);
    }

    bool isGrounded(float distanceToGround)
    {
        return Physics.Raycast(transform.position, -Vector3.up, distanceToGround + 0.1f);
    }
}
