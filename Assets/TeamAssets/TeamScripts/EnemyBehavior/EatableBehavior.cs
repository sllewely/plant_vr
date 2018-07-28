using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EatableBehavior : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("beetle trigger in region " + other.gameObject.name);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("beetle collision in region" + collision.gameObject.name);
    }
}
