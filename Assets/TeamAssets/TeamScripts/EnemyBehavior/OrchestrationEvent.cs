using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrchestrationEvent : MonoBehaviour
{
	private enum OrchestrationState { Start, Before, During, After }

	private OrchestrationState currentState;
	
	public float beginAfterTime;
	public float duration;
	public List<GameObject> orchestratables; 

	// Use this for initialization
	private void Start ()
	{
		currentState = OrchestrationState.Start;
		Invoke(GetFunctionName(ActivateEvents), beginAfterTime);
	}

	private void ActivateEvents()
	{
		orchestratables.ForEach(obj => obj.SetActive(true));
		Invoke(GetFunctionName(EndEvents), duration);
	}

	private void EndEvents()
	{
		orchestratables.ForEach(obj => obj.SetActive(false));
		Destroy(gameObject);
	}
	
	private static string GetFunctionName(Action method)
	{
		return method.Method.Name;
	}
}
