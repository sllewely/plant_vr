using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState : MonoBehaviour {

    int thingsEatenCounter = 0;
    int score;
    public int health;
    public GameObject gameUi;

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
        SetScoreUi();
        Debug.Log("wow I ate " + thingsEatenCounter + " things and have " + score + "points");
    }

    private void SetScoreUi()
    {
        gameUi = GameObject.Find("ScreenUi");
        gameUi.GetComponent<ScoreTextBehavior>().SetScore(score);
    }
}
