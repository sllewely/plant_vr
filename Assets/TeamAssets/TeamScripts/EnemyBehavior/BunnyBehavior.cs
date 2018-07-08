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
    Quaternion rotateRight = Quaternion.Euler(new Vector3(0, 50, 0));
    Quaternion rotateLeft = Quaternion.Euler(new Vector3(0, -50, 0));

    private IEnumerator jumpingBunny;

    // Use this for initialization
    void Start () {
        rigidBody = GetComponent<Rigidbody>();
        StartCoroutine(JumpingBunny());
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
                rigidBody.rotation = rotateRight;
            } else
            {
                rigidBody.rotation = rotateLeft;
            }
        }
        isGrounded = false;
        rigidBody.AddForce(jumpVector);
    }

    IEnumerator JumpingBunny()
    {
        for (; ; )
        {
            if (isGrounded) {
                BunnyJump();
            }
            yield return new WaitForSeconds(0.5f);
        }
    }
}
