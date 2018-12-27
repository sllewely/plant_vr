using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class EnemyOrchestrator : MonoBehaviour
{

	public WaveSequence[] waveSequences;
	
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
		// init wave sequences
		InitWaveSequences();
	}

	void InitWaveSequences()
	{
		foreach(var waveSequence in waveSequences)
		{
			// todo: handle offset
			waveSequence.waveEvents.ForEach(waveEvent => waveEvent.spawnerNames.ForEach(spawnerName =>
				{
					if (spawners.ContainsKey(spawnerName))
					{
						StartCoroutine(StartSpawner(spawners[spawnerName], waveEvent.eventTime, waveEvent.on));
					}
					else
					{
						Debug.Log("spawner not found: " + spawnerName);
					}
				}
			));
		}
	}
	
	private IEnumerator StartSpawner(Orchestratable spawner, float time, bool onSetting)
	{
		yield return new WaitForSeconds(time);
		if (onSetting)
		{
			spawner.BeginEvent();
		}
		else
		{
			spawner.EndEvent();
		}
	}
}
