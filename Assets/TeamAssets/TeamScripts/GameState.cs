using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState : MonoBehaviour {

    public int thingsEatenCounter = 0;

    public void eatSomething(GameObject theThing)
    {
        // I'm imaginging something like score += theThing.pointValue
        thingsEatenCounter += 1;
        Debug.Log("wow I ate " + thingsEatenCounter + " things!");
    }
}
