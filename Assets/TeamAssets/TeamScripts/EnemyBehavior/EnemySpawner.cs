using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {
    // Very simple enemy spawner

    //Vector3 startingPos;
    public float timeInterval;
    public float timeToDestroy;

    int waveCount;
    public float timeBetweenWaves;
    public int minWave;
    public int maxWave;

    private Bounds spawnBounds;

    public GameObject[] enemies;

	// Use this for initialization
	void Start () {

        //startingPos = transform.position;
        spawnBounds = GetComponent<Collider>().bounds;
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
        float x = Random.Range(spawnBounds.min.x, spawnBounds.max.x);
        float y = Random.Range(spawnBounds.min.y, spawnBounds.max.y);
        float z = Random.Range(spawnBounds.min.z, spawnBounds.max.z);
        Vector3 spawnPosition = new Vector3(x, y, z);

        GameObject newEnemy = Instantiate(NextEnemy(), spawnPosition, transform.rotation);
        Destroy(newEnemy, timeToDestroy);
    }

    GameObject NextEnemy()
    {
        return enemies[Random.Range(0, enemies.Length - 1)];
    }
}
