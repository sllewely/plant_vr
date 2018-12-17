using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class CsvEnemyOrchestrator : ExtendMonoBehaviour
{
	readonly Regex COMMENT = new Regex(@"^#");
	readonly Regex INSTRUCTION = new Regex(@"^(?<on>(on)|(off)), ?(?<time>\b\d+\b)(, ?(?<spawnerName>\b\w+\b))+ ?$");

	public String orchestrationFile;
	
	private Dictionary<String, Orchestratable> spawners = new Dictionary<String, Orchestratable>();

	private void Start()
	{
		Debug.Log(Path());
		BuildSpawnerDict();
		ProcessFile();
	}

	private void BuildSpawnerDict()
	{
		var orchestratables = GameObject.FindGameObjectsWithTag("Spawner");
		foreach (var spawner in orchestratables)
		{
			spawners.Add(spawner.name, spawner.GetComponent<Orchestratable>());
		}
	}

	private void ProcessFile()
	{
		string[] lines = System.IO.File.ReadAllLines(Path());
		foreach (var line in lines)
		{
			if (COMMENT.IsMatch(line))
			{
				continue;
			}

			if (!INSTRUCTION.IsMatch(line))
			{
				Debug.Log("no match from line: " + line);
				continue;
			}
			CreateEventsFromMatch(INSTRUCTION.Match(line));
		}
	}

	private void CreateEventsFromMatch(Match match)
	{
		bool onSetting = match.Groups["on"].Value.Equals("on");
		var spawnerNames = match.Groups["spawnerName"].Captures;
		float time = float.Parse(match.Groups["time"].Value);
		foreach (Capture spawnerName in spawnerNames)
		{
			if (spawners.ContainsKey(spawnerName.Value))
			{
				StartCoroutine(StartSpawner(spawners[spawnerName.Value], time, onSetting));
			}
			else
			{
				Debug.Log("spawner not found: " + spawnerName);
			}
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

	private String Path()
	{
		var dir = System.IO.Path.Combine(
			System.IO.Path.Combine(
				System.IO.Path.Combine(
					System.IO.Path.Combine(
						System.IO.Directory.GetParent(Environment.CurrentDirectory).ToString(),
						"plant_vr"),
					"Assets"),
				"TeamAssets"),
			"TeamCsvs");
		
		return System.IO.Path.Combine(dir, orchestrationFile);
	}
}
