using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class CsvEnemyOrchestrator : ExtendMonoBehaviour
{
	readonly Regex COMMENT = new Regex(@"^#");
	readonly Regex INSTRUCTION = new Regex(@"^(?<on>(on)|(off)), ?(?<spawnerName>\b\w+\b), ?(?<time>\b\d+\b) ?$");

	public String orchestrationFile;
	
	private Dictionary<String, Orchestratable> spawners = new Dictionary<String, Orchestratable>();

	private void Start()
	{
		Debug.Log(Path());
		BuildSpawnerDict();
		ProcessFile();
		foreach (var spawnerName in spawners.Keys)
		{
			Debug.Log("spawner: " + spawnerName);
		}

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
		
			Match match = INSTRUCTION.Match(line);
			var onSetting = match.Groups["on"].Equals("on");
			var spawnerName = match.Groups["spawnerName"];
			var time = match.Groups["time"];
			
			Debug.Log("Line found match : " + spawnerName);

		}
	}

	private void CreateEvent(bool onSetting, String spawnerName, float time)
	{
		
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
