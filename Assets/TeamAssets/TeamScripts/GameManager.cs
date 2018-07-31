using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public GameObject gameMenu;
    public GameObject menuHand;

    int lives;
    Vector3 offset = new Vector3(0, 0.5f, 0);

    // Plants that represent lives
    public GameObject[] plantLives;
    public GameObject deadPlantPrefab;

    private void Start()
    {
        lives = plantLives.Length;
    }

    // Update is called once per frame
    void Update () {
        // TODO: input
        if (Input.GetKeyDown(KeyCode.Space))
        {
            LoseLife();   
        }
		
	}

    void LoseLife()
    {
        if (IsDead())
        {
            Debug.LogWarning("Already dead! Lives: " + lives);
            return;
        }

        lives--;
        GameObject plantToKill = plantLives[lives];
        Transform plantPosition = plantToKill.transform;
        Instantiate(deadPlantPrefab, plantPosition.position + offset, plantPosition.rotation);
        Destroy(plantToKill);
    }

    bool IsDead()
    {
        return lives <= 0;
    }

    void ToggleMenu()
    {
        bool isMenuActice = gameMenu.activeSelf;
        gameMenu.SetActive(!isMenuActice);
        menuHand.SetActive(!isMenuActice);
    }
}
