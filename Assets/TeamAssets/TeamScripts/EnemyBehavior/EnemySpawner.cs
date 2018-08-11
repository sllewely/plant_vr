using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {
    // Very simple enemy spawner

    Vector3 startingPos;
    public float timeInterval;
    public float timeToDestroy;

    public GameObject enemy;

	// Use this for initialization
	void Start () {
        
        startingPos = transform.position;
        //  StartCoroutine(Spawner());
        StartCoroutine(Spawner());
    }

    // Update is called once per frame
    void Update () {
		
	}

    IEnumerator Spawner()
    {
        for ( ; ; )
        {
            Spawn();
            yield return new WaitForSeconds(timeInterval);
        }

    }

    void Spawn()
    {
        GameObject newEnemy = Instantiate(enemy, transform.position, transform.rotation);
        Destroy(newEnemy, timeToDestroy);
    }
}
