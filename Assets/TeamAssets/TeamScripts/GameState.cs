using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState : MonoBehaviour {

    int thingsEatenCounter = 0;
    int score = 0;
    int health = 3;

    private void Start()
    {

    }

    public void EatSomething(GameObject theThing)
    {
        // I'm imaginging something like score += theThing.pointValue
        this.thingsEatenCounter += 1;
        EatableBehavior eatableBehavior = theThing.GetComponent<EatableBehavior>();
        this.score += eatableBehavior.points;
        SetScoreUi();
        PrintScore();
    }

    public void PrintScore()
    {
        Debug.Log("wow I ate " + thingsEatenCounter + " things and have " + score + "points");
    }

    private void SetScoreUi()
    {
        GameObject gameUi = GameObject.Find("ScreenUi");
        gameUi.GetComponent<ScoreTextBehavior>().SetScore(score);
    }
}
