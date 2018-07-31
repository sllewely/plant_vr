using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EatableBehavior : MonoBehaviour {
    // the points this prey is worth
    public int points;
    public GameManager gameManager;

	// Use this for initialization
	void Start () {
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "EatingRegion")
        {
            Debug.Log("Beetle Eatable inside of EatableRegion");
            // Add the score
            gameManager.GetComponent<GameState>().EatSomething(gameObject);
            // Deactivate the hand
            Grab grab = GetComponentInParent<Grab>();
            if (grab != null)
                grab.isHolding = false;

            Destroy(gameObject);
        }
    }

}
