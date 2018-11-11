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
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (aiOn)
            {
                ToggleAiOff();
            }
            else
            {
                ToggleAiOn();
            }
        }

        
	}

    private void FixedUpdate()
    {
        if (aiOn)
        {
            Act();
        }
    }

    public virtual void Act() {}

    public virtual void Setup() { }

    public void ToggleAiOn()
    {
        Debug.Log("ai on for " + name);
        aiOn = true;
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.isKinematic = true;
    }

    public void ToggleAiOff()
    {
        Debug.Log("ai off for " + name);
        aiOn = false;
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.isKinematic = false;
    }
}
