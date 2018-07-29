using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreTextBehavior : MonoBehaviour {

    public Text scoreText;

	// Use this for initialization
	void Start () {
        scoreText.text = "Score: 0";
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SetScore(int score)
    {
        scoreText.text = "Score: " + score;
    }
}
