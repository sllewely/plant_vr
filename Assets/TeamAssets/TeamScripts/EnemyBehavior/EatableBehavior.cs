using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EatableBehavior : MonoBehaviour {
    GameObject eatingRegion;
    Collider eatingRegionCollider;

	// Use this for initialization
	void Start () {
        GameObject eatingRegion = GameObject.FindGameObjectsWithTag("EatingRegion")[0];
        eatingRegionCollider = eatingRegion.GetComponent<Collider>();
        //Debug.Log("eatingRegion: " + eatingRegion.name);
        //Debug.Log("bounds: " + eatingRegionCollider.bounds);

    }
	
	// Update is called once per frame
	void Update () {
        if (InEatingRegion())
        {
            Debug.Log(gameObject.name + " eatable in region");
        }
		
	}

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("beetle trigger in region " + other.gameObject.name);
        if (other.tag == "EatableRegion")
        {

        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("beetle collision in region" + collision.gameObject.name);
    }

    private bool InEatingRegion()
    {
       // Debug.Log("bounds: " + eatingRegionCollider.bounds + " for position: " + transform.position);
        return eatingRegionCollider.bounds.Contains(transform.position);
    }
}
