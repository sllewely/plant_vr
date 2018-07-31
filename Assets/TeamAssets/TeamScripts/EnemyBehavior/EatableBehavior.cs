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
            Debug.Log("Beetle Eatable inside of EatableRegion");
            // Add the score
            gameManager.GetComponent<GameState>().EatSomething(gameObject);
            // Deactivate the hand

            Destroy(gameObject);
        }
    }

}
