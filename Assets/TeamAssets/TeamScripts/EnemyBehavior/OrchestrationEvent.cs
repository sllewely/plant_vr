using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrchestrationEvent : ExtendMonoBehaviour
{
	public float beginAfterTime;
	public float duration;
	public List<GameObject> orchestratables; 

	// Use this for initialization
	private void Start ()
	{
		InvokeAction(ActivateEvents, beginAfterTime);
	}

	private void ActivateEvents()
	{
		Debug.Log("begin wave " + gameObject.name);
		orchestratables.ForEach(obj => obj.SetActive(true));
		InvokeAction(EndEvents, duration);
	}

	private void EndEvents()
	{
		Debug.Log("Ending wave " + gameObject.name);
		orchestratables.ForEach(obj => obj.SetActive(false));
		Destroy(gameObject);
	}
}
