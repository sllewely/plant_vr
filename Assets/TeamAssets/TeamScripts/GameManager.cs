using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public GameObject gameMenu;
    public GameObject menuHand;
	
	// Update is called once per frame
	void Update () {
        // TODO: input
        if (Input.GetKeyDown(KeyCode.Space))
        {
            bool isMenuActice = gameMenu.activeSelf;
            gameMenu.SetActive(!isMenuActice);
            menuHand.SetActive(!isMenuActice);
        }
		
	}

    int thingsEatenCounter;
    int score;
    int health;

    private void Awake()
    {
        score = 0;
        thingsEatenCounter = 0;
        health = 3;
        Debug.Log("start with score: " + score);
    }

    private void Start()
    {

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
        GameObject gameUi = GameObject.Find("ScreenUi");
        gameUi.GetComponent<ScoreTextBehavior>().SetScore(score);
    }
}
