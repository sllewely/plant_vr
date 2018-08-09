using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PreyBehavior : MonoBehaviour {
    private bool aiOn;

	// Use this for initialization
	void Start () {
        aiOn = true;
        Setup();
		
	}
	
	// Update is called once per frame
	void Update () {
        Debug.Log("aiOn is " + aiOn);
        if (aiOn) {
            Act();
        }
        
	}

    public virtual void Act() {}

    public virtual void Setup() { }

    public void ToggleAiOn()
    {
        aiOn = true;
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.isKinematic = true;
    }

    public void ToggleAiOff()
    {
        aiOn = false;
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.isKinematic = false;
    }
}
