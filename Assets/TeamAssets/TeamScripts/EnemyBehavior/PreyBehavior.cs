using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreyBehavior : MonoBehaviour {
    private bool aiOn;

	// Use this for initialization
	void Start () {
        aiOn = true;
		
	}
	
	// Update is called once per frame
	void Update () {
        Act();
	}

    public virtual void Act() {}

    public void ToggleAiOn()
    {
        aiOn = true;

    }
}
