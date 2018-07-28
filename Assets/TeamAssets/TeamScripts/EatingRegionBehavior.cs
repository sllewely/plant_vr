using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EatingRegionBehavior : MonoBehaviour {

    // public GameObject gameStateObject;
    // public AudioSource omnomnom;

    private void OnTriggerStay(Collider other)
    {
        Debug.Log("trigger stay");
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("On trigger enter");
        // perhaps raw strings like "Prey" should belong to some class
        // and called like other.tag == TeamTags.Prey
        if (other.tag == "Prey") {
            // omnomnom.Play();
            GameObject prey = other.gameObject;
            // gameStateObject.GetComponent<GameState>().eatSomething(prey);
            Destroy(prey);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("collision detected");
    }
}
