using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerMouseMovement : MonoBehaviour {

    // Only for testing while Sarah is working on mouse/keyboard instead of a VR headset like a pleb
    // GameObject menuHand;

    public float speed = .1f;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.A))
        {
            transform.Translate(Vector3.right * speed);
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            transform.Translate(Vector3.left * speed);
        }
    }
}
