using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState : MonoBehaviour {

    public int thingsEatenCounter = 0;
    public int score;
    public int health;

    private void Start()
    {
        score = 0;
        thingsEatenCounter = 0;
        health = 3;
    }

    public void EatSomething(GameObject theThing)
    {
        // I'm imaginging something like score += theThing.pointValue
        thingsEatenCounter += 1;
        EatableBehavior eatableBehavior = theThing.GetComponent<EatableBehavior>();
        score += eatableBehavior.points;
        Debug.Log("wow I ate " + thingsEatenCounter + " things and have " + score + "points");
    }
}
