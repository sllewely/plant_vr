using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveEvent : MonoBehaviour
{
	public bool on;
	public int eventTime;
	public Orchestratable[] spawners;

	public WaveEvent(bool on, int eventTime, Orchestratable[] spawners)
	{
		this.on = on;
		this.eventTime = eventTime;
		this.spawners = spawners;
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
