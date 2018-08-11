using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {
    // Very simple enemy spawner

    Vector3 startingPos;
    public float timeInterval;
    public float timeToDestroy;

    int waveCount;
    public float timeBetweenWaves;
    public int minWave;
    public int maxWave;

    public GameObject enemy;

	// Use this for initialization
	void Start () {
        
        startingPos = transform.position;
        waveCount = Random.Range(minWave, maxWave);
        StartCoroutine(Spawner());
    }

    // Update is called once per frame
    void Update () {
		
	}

    IEnumerator Spawner()
    {
        for ( ; ; )
        {
            waveCount--;
            Spawn();
            if (waveCount <= 0)
            {
                waveCount = Random.Range(minWave, maxWave);
                yield return new WaitForSeconds(timeBetweenWaves + timeInterval);
            }
            else
            {
                yield return new WaitForSeconds(timeInterval);
            }
        }

    }

    void Spawn()
    {
        GameObject newEnemy = Instantiate(enemy, transform.position, transform.rotation);
        Destroy(newEnemy, timeToDestroy);
    }
}
