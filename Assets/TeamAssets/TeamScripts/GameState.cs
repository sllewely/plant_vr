using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState : MonoBehaviour {

    int thingsEatenCounter = 0;
    int score = 15;
    public int targetScore;
    int health = 3;

    private GameObject feedNeedle;

    private void Start()
    {
        feedNeedle = GameObject.Find("Feed_meter/Needle");
        RotateNeedle();
    }

    public void EatSomething(GameObject theThing)
    {
        // I'm imaginging something like score += theThing.pointValue
        this.thingsEatenCounter += 1;
        EatableBehavior eatableBehavior = theThing.GetComponent<EatableBehavior>();
        this.score += eatableBehavior.points;
        SetScoreUi();
        RotateNeedle();
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

    private void RotateNeedle()
    {
        Debug.Log("Setting rot for " + feedNeedle.name);
//        var currentRot = feedNeedle.transform.rotation;
        var z = score / (float)targetScore * 180;
        feedNeedle.transform.localRotation = Quaternion.Euler(0, 0, z);
    }
}
