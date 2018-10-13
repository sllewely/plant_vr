using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EatableBehavior : MonoBehaviour {
    // the points this prey is worth
    public int points;
    GameObject gameManager;

	// Use this for initialization
	void Start () {
        gameManager = GameObject.Find("GameManager");
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "EatingRegion")
        {
            Debug.Log(name + " inside of EatableRegion");

            // Only eat bug if it's currently being held
            Grab grab = GetComponentInParent<Grab>();
            if (grab != null) {
                // Add the score
                gameManager.GetComponent<GameState>().EatSomething(gameObject);
                other.GetComponent<AudioSource>().Play();
                other.GetComponentInChildren<ParticleSystem>().Play();
                // Deactivate the hand
                grab.isHolding = false;

                Destroy(gameObject);
            }

        }
    }

}
