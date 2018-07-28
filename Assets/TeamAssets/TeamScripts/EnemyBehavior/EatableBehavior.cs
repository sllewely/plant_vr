using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EatableBehavior : MonoBehaviour {
    // the points this prey is worth
    public int points;

	// Use this for initialization
	void Start () {
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("beetle trigger in region " + other.gameObject.name);
        if (other.tag == "EatableRegion")
        {
            // Add the score
            // Delete the object
            // Deactivate the hand
        }
    }

}
