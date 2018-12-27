using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Networking;

public class MasterSceneWaveSequence : WaveSequence {
	
//# on/off, time at, spawner
//# first wave
//on, 3, SkullSpawner
//off, 28, SkullSpawner
//on, 20, GroundhogSpawner
//off, 25, GroundhogSpawner
//# second wave
//on, 35, MothSpawner_B, SkullSpawner2, SkullSpawner3
//off, 90, MothSpawner2
//off, 90, SkullSpawner2
//off, 90, SkullSpawner3
//on, 60, GroundhogSpawner
//off, 80, GroundhogSpawner
//# Third wave
//on, 80, SkullSpawner, MothSpawner_B, MothSpawner
//off, 100, SkullSpawner, MothSpawnerB, MothSpawner
//on, 90, HardGroundhogSpawner
//off, 125, HardGroundhogSpawner

	private static string[] skullSpawner = {"MiddleSkullSpawner"};
	private static string[] groundhogSpawner = {"GroundhogSpawner"};

	private static string[] flyingSpawners = {"RightMothBSpawner", "LeftSkullFlyBSpawner", "RightMothSpawner"};

	private static string[] hardGroundhogSpawner = {"HardGroundhogSpawner"};
//	public WaveEvent[] waveEvents =
//	{
//		new WaveEvent(true, 3, skullSpawner),
//		new WaveEvent(false, 28, skullSpawner), 
//	};

	override protected void Init()
	{
		waveEvents.Add(new WaveEvent(true, 3, skullSpawner));
		waveEvents.Add(new WaveEvent(false, 28, skullSpawner));
		waveEvents.Add(new WaveEvent(true, 20, groundhogSpawner));
		waveEvents.Add(new WaveEvent(false, 25, groundhogSpawner));
		// second wave
		waveEvents.Add(new WaveEvent(true, 35, flyingSpawners));
		waveEvents.Add(new WaveEvent(false, 70, flyingSpawners));
		waveEvents.Add(new WaveEvent(true, 60, groundhogSpawner));
		waveEvents.Add(new WaveEvent(false, 80, groundhogSpawner));
		// third wave
		waveEvents.Add(new WaveEvent(true, 80, flyingSpawners));
		waveEvents.Add(new WaveEvent(false, 100, flyingSpawners));
		waveEvents.Add(new WaveEvent(true, 90, hardGroundhogSpawner));
		waveEvents.Add(new WaveEvent(false, 125, hardGroundhogSpawner));
	}
}
