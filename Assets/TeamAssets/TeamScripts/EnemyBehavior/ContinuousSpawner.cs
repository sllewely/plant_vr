using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContinuousSpawner : MonoBehaviour, Orchestratable {
    
    private bool isSpawning;
    private Coroutine spawningCoroutine;

    public GameObject enemy;
    private Bounds spawnBounds;
    public float timeInterval;
    private float timeToDestroy = 20f;

    // randomly vary size
    public float minScalePct;
    public float maxScalePct;

    private void Start()
    {
        spawnBounds = GetComponent<Collider>().bounds;
        isSpawning = false;
    }

    public void BeginEvent()
    {
        if (isSpawning)
        {
            Debug.LogWarning("Spawner " + name + " already active at time " + Time.time);
            return;
        }
        isSpawning = true;
        spawningCoroutine = StartCoroutine(Spawner());
        
    }

    public void EndEvent()
    {
        if (!isSpawning)
        {
            Debug.LogWarning("Spawner " + name + " already off at time " + Time.time);
            return;
        }
        isSpawning = false;
        StopCoroutine(spawningCoroutine);
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
        Vector3 spawnPosition = GetRandomFromBox();

        var scale = (maxScalePct == 0) ? 1 : Random.Range(minScalePct * 100, maxScalePct * 100) / 100f;
        GameObject newEnemy = Instantiate(enemy, spawnPosition, transform.rotation);
        newEnemy.transform.localScale = newEnemy.transform.localScale * scale;
        
        Destroy(newEnemy, timeToDestroy);
    }

    Vector3 GetRandomFromBox()
    {
        float x = Random.Range(spawnBounds.min.x, spawnBounds.max.x);
        float y = gameObject.transform.position.y;
        float z = Random.Range(spawnBounds.min.z, spawnBounds.max.z);
        return new Vector3(x, y, z);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(transform.position, .2f);
        Gizmos.DrawLine(transform.position, (transform.forward * 3) + transform.position);
    }
}
