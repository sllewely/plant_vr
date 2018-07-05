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
    public int hopCycle = 3;
    private int currentHop = 0;
    public bool hopLeft = true;

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
        currentHop--;
        if (currentHop <= 0)
        {
            currentHop = hopCycle;
            hopLeft = !hopLeft;
            if (!hopLeft) {
                Quaternion deltaRotation = Quaternion.Euler(new Vector3(0, 50, 0));
                rigidBody.rotation = deltaRotation;
                Debug.Log("Bunny rotate");
            } else
            {
                Quaternion deltaRotation = Quaternion.Euler(new Vector3(0, -50, 0));
                rigidBody.rotation = deltaRotation;
                Debug.Log("Bunny rotate");
            }

        }
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
                Debug.Log("Grounded!");
                BunnyJump();
            } else
            {
                Debug.Log("Jumping!");
            }
            yield return new WaitForSeconds(0.5f);
        }
    }
}
