using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveEvent
{
	public bool on;
	public int eventTime;
//	public Orchestratable[] spawners;
	public string[] spawnerNames;

	public WaveEvent(bool on, int eventTime, string[] spawnerNames)
	{
		this.on = on;
		this.eventTime = eventTime;
		this.spawnerNames = spawnerNames;
	}

}
