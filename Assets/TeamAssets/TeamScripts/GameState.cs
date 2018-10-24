using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    
    // public for debugging
    public void AddScore(int change)
    {
        score += change;
        SetScoreUi();
        RotateNeedle();
//        PrintScore();
    }

    public void EatSomething(GameObject theThing)
    {
        // I'm imaginging something like score += theThing.pointValue
        thingsEatenCounter += 1;
        EatableBehavior eatableBehavior = theThing.GetComponent<EatableBehavior>();
        AddScore(eatableBehavior.points);
    }

    public void PrintScore()
    {
        Debug.Log("wow I ate " + thingsEatenCounter + " things and have " + score + " points");
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
                AddScore(-scoreDrain);
            }
            
            yield return new WaitForSeconds(timeBetweenDrain);
        }
    }

    private void VictoryOn()
    {
        GameObject.Find("ScreenUi/WinText").GetComponent<Text>().text = "Victory!";
        GetComponent<GameManager>().SetGameOver();
    }
}
