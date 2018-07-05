using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class BunnyBehavior : MonoBehaviour {

    Rigidbody rigidBody;
    Vector3 jumpVector = new Vector3(50f, 100f, 0);
    public float onGroundDistance = 0.5f;
    public bool isJumping = false;
    public bool isGrounded = true;

    private IEnumerator jumpingBunny;


    // Use this for initialization
    void Start () {
        rigidBody = GetComponent<Rigidbody>();
        if (IsGrounded())
        {
            Debug.Log("bunny on the ground");
            BunnyJump();
        } else
        {
            Debug.Log("bunny not on the ground");
        }

        jumpingBunny = JumpingBunny();
        StartCoroutine(jumpingBunny);

    }
	
	// Update is called once per frame
	void Update () {
        
    }

    private void OnCollisionStay(Collision collision)
    {
        isGrounded = true;
    }

    void BunnyJump()
    {
        isGrounded = false;
        Debug.Log("Bunny jump!!");
        rigidBody.AddForce(jumpVector);
    }

    bool IsGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, onGroundDistance);
    }

    IEnumerator JumpingBunny()
    {
        Debug.Log("jimping bunny");
        for (; ; )
        {
            if (isGrounded) {
                Debug.Log("Grounded!  And jump!");
                BunnyJump();
            } else
            {
                Debug.Log("Jumping!");
            }
            yield return new WaitForSeconds(0.5f);
        }
    }
}
