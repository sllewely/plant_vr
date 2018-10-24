using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState : MonoBehaviour {

    int thingsEatenCounter = 0;
    public int startScore;
    int score;
    public int scoreDrain;
    public int timeBetweenDrain;
    
    public int targetScore;
    int health = 3;
    

    private GameObject feedNeedle;

    private void Start()
    {
        score = startScore;
        feedNeedle = GameObject.Find("Feed_meter/Needle");
        RotateNeedle();
        StartCoroutine(DrainScore());
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
        GameObject gameUi = GameObject.Find("ScoreUi");
        gameUi.GetComponent<ScoreTextBehavior>().SetScore(score);
    }

    private void RotateNeedle()
    {
        var z = score / (float)targetScore * 180;
        if (z > 180)
        {
            VictoryOn();
            z = 180;
        }
        feedNeedle.transform.localRotation = Quaternion.Euler(0, 0, z);
    }
    
    // Steadily drains the feed meter
    private IEnumerator DrainScore()
    {
        while (true)
        {
            if (score - scoreDrain >= 0)
            {
                score -= scoreDrain;
                RotateNeedle();
            }
            
            yield return new WaitForSeconds(timeBetweenDrain);
        }
    }

    private void VictoryOn()
    {
        GameObject.Find("ScoreUI/WinText").SetActive(true);
    }
}
