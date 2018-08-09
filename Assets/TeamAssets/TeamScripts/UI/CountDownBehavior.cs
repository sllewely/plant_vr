using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountDownBehavior : MonoBehaviour {

    private int countDown = 0;
    public Text countDownText;
    private int timeLimit;

	// Use this for initialization
	void Start () {
        countDownText.text = "";
        timeLimit = GameObject.Find("GameManager").GetComponent<GameManager>().timeLimit;
    }
	
	// Update is called once per frame
	void Update () {
		if (Time.time > timeLimit)
        {
            ClearCountDown();
        } else if (Time.time > timeLimit - 1)
        {
            SetCountDown(1);
        }
        else if (Time.time > timeLimit - 2)
        {
            SetCountDown(2);
        }
        else if (Time.time > timeLimit - 3)
        {
            SetCountDown(3);
        }
    }

    public void ClearCountDown()
    {
        if (countDown != 0)
        {
            countDownText.text = "";
            countDown = 0;

        }
    }

    public void SetCountDown(int count)
    {
        if (countDown != count)
        {
            countDownText.text = count.ToString();
            countDown = count;
        }
        
    }
}
