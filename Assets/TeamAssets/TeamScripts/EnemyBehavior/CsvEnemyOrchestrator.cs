using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CsvEnemyOrchestrator : MonoBehaviour
{

	public String orchestrationFile;

	private void Start()
	{
		Debug.Log(Path());
		ProcessFile();
	}

	private void ProcessFile()
	{
		string[] lines = System.IO.File.ReadAllLines(Path());
	}

	private String Path()
	{
		var dir = System.IO.Path.Combine(
			System.IO.Path.Combine(
				System.IO.Path.Combine(
					System.IO.Path.Combine(
						System.IO.Directory.GetParent(Environment.CurrentDirectory).ToString(), "plant_vr"),
					"Assets"),
				"TeamAssets"),
			"TeamCsvs");
		
		return System.IO.Path.Combine(dir, orchestrationFile);
	}
}
