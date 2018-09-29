using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFreezeForLaserGopher : MonoBehaviour {
	
	// If freeze game is on
	// Detect movement

	private bool freezeTime;

	// Use this for initialization
	void Start ()
	{
		freezeTime = false;
	}

	void FixedUpate()
	{
		
	}
	
	public void BeginFreezeTime()
	{
		Debug.Log("begin freeze time");
		freezeTime = true;
	}

	public void EndFreezeTime()
	{
		Debug.Log("end freeze time");
		freezeTime = false;
	}
}
