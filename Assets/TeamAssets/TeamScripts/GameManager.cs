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
}
