using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public int timeLimit;
    private float timeLeftInGame;

    public GameObject gameMenu;
    public GameObject menuHand;

    public GameObject gameOver;

    int lives;
    Vector3 offset = new Vector3(0, 0.5f, 0);

    // Plants that represent lives
//    public GameObject[] plantLives;
//    public GameObject deadPlantPrefab;
    
    private void Start()
    {
        timeLeftInGame = timeLimit;
//        lives = plantLives.Length;
    }

    // Update is called once per frame
    void Update () {
        timeLeftInGame -= Time.deltaTime;
        // TODO: input
//        if (Input.GetKeyDown(KeyCode.Space))
//        {
//            LoseLife();   
//        }
		
        if (Input.GetKeyDown(KeyCode.R))
        {
            RestartGame();
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            GetComponent<GameState>().AddScore(15);
        }

        if (timeLeftInGame < 0)
        {
            Debug.Log("Time is up!");
            SetGameOver();
        }
	}

//    void LoseLife()
//    {
//        if (IsDead())
//        {
//            Debug.LogWarning("Already dead! Lives: " + lives);
//            return;
//        }
//
//        lives--;
//        GameObject plantToKill = plantLives[lives];
//        Transform plantPosition = plantToKill.transform;
//        Instantiate(deadPlantPrefab, plantPosition.position + offset, plantPosition.rotation);
//        Destroy(plantToKill);
//        if (IsDead())
//        {
//            SetGameOver();
//        }
//    }

//    bool IsDead()
//    {
//        return lives <= 0;
//    }

    public void SetGameOver()
    {
        gameOver.SetActive(true);
        GameObject[] spawners = GameObject.FindGameObjectsWithTag("Spawner");
        foreach (var spawner in spawners)
        {
            spawner.SetActive(false);
        }
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Prey");
        foreach (var enemy in enemies)
        {
            Destroy(enemy);
        }
    }

    void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name, LoadSceneMode.Single);
    }

    void ToggleMenu()
    {
        bool isMenuActice = gameMenu.activeSelf;
        gameMenu.SetActive(!isMenuActice);
        menuHand.SetActive(!isMenuActice);
    }
}
