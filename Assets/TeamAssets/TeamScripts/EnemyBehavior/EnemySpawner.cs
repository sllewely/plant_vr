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

    public float minScalePct;
    public float maxScalePct;

    public List<GameObject> spawnLocations;

    private Bounds spawnBounds;

    public GameObject[] enemies;


    void OnEnable()
    {
        Init();
    }

    private void Init()
    {
        spawnBounds = GetComponent<Collider>().bounds;
        waveCount = Random.Range(minWave, maxWave);
        StartCoroutine(Spawner());
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
        Vector3 spawnPosition = spawnLocations.Count > 0 ? GetRandomPosFromLocations() : GetRandomFromBox();

        var scale = (maxScalePct == 0) ? 1 : Random.Range(minScalePct * 100, maxScalePct * 100) / 100f;
        GameObject newEnemy = Instantiate(NextEnemy(), spawnPosition, transform.rotation);
        newEnemy.transform.localScale = newEnemy.transform.localScale * scale;
        
        Destroy(newEnemy, timeToDestroy);
    }

    GameObject NextEnemy()
    {
        return enemies[Random.Range(0, enemies.Length)];
    }

    Vector3 GetRandomFromBox()
    {
        float x = Random.Range(spawnBounds.min.x, spawnBounds.max.x);
        float y = gameObject.transform.position.y;
        float z = Random.Range(spawnBounds.min.z, spawnBounds.max.z);
        return new Vector3(x, y, z);
    }

    Vector3 GetRandomPosFromLocations()
    {
        return spawnLocations[Random.Range(0, spawnLocations.Count)].transform.position;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(transform.position, .2f);
        Gizmos.DrawLine(transform.position, (transform.forward * 3) + transform.position);
    }
}
