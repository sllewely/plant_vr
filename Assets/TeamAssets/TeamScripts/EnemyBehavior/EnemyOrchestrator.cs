using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyOrchestrator : MonoBehaviour {
	
	
	private Dictionary<String, Orchestratable> spawners = new Dictionary<String, Orchestratable>();
	
	private void BuildSpawnerDict()
	{
		// Or get Spawners from children of child component Spawners
		var orchestratables = GameObject.FindGameObjectsWithTag("Spawner");
		foreach (var spawner in orchestratables)
		{
			spawners.Add(spawner.name, spawner.GetComponent<Orchestratable>());
		}
	}

	// Use this for initialization
	void Start () {
		BuildSpawnerDict();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
