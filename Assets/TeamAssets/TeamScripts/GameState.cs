using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameState : MonoBehaviour {

    int thingsEatenCounter = 0;
    public int startScore;
    int score;
    int scoreDrain;
    private float drainDelay;
    private bool drainBegin;
    public float timeBetweenDrain;

    int targetScore;
    int health;

    public int startingHealthEasy;
    public int scoreDrainEasy;
    public int targetScoreEasy;
    public float drainDelayEasy;

    public int startingHealthNormal;
    public int scoreDrainNormal;
    public int targetScoreNormal;
    public float drainDelayNormal;

    private float drainWaitTime = 0;


    private GameObject feedNeedle;

    private void Start()
    {
        drainBegin = false;
        score = startScore;
        feedNeedle = GameObject.Find("Feed_meter/Needle");
        SetNormalDifficulty();
        RotateNeedle();
        //StartCoroutine(DrainScore());
    }

    private void Update()
    {
        if (!drainBegin)
        {
            if (drainWaitTime >= drainDelay)
            {
                drainBegin = true;
                StartCoroutine(DrainScore());
            }
            else //counting up rather than down to accommodate changing delay in tutorial
                drainWaitTime += Time.deltaTime;             
        }


        if (Input.GetKeyDown("1"))
        {
            SetEasyDifficulty();
        }
        if (Input.GetKeyDown("2"))
        {
            SetNormalDifficulty();
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            TakeDamage();
        }
    }

    public int GetScore()
    {
        return score;
    }

    public void SetScore(int newScore)
    {
        score = newScore;
    }

    public void SetEasyDifficulty()
    {
        Debug.Log("Difficulty Easy");
        health = startingHealthEasy;
        scoreDrain = scoreDrainEasy;
        drainDelay = drainDelayEasy;
        targetScore = targetScoreEasy;
        RotateNeedle();
    }

    public void SetNormalDifficulty()
    {
        Debug.Log("Difficulty Normal");
        health = startingHealthNormal;
        scoreDrain = scoreDrainNormal;
        drainDelay = drainDelayNormal;
        targetScore = targetScoreNormal;
        RotateNeedle();
    }
    
    // public for debugging
    public void AddScore(int change)
    {
        score += change;
        RotateNeedle();
    }

    public void EatSomething(GameObject theThing)
    {
        // I'm imaginging something like score += theThing.pointValue
        thingsEatenCounter += 1;
        EatableBehavior eatableBehavior = theThing.GetComponent<EatableBehavior>();
        AddScore(eatableBehavior.points);
    }

    public void TakeDamage()
    {
        health -= 1;
        if (health <= 0)
        {
            GetComponent<GameManager>().SetGameOver("Too much damage!");
        }
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
        SetScoreUi();
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
            else
            {
                AddScore(-score);
                GameOver("You starved...");   
            }
            
            yield return new WaitForSeconds(timeBetweenDrain);
        }
    }

    private void VictoryOn()
    {
        GameOver("You won!!!");
    }

    private void GameOver(String gameOverText)
    {
        StopCoroutine(DrainScore());
        GetComponent<GameManager>().SetGameOver(gameOverText);
    }
}
