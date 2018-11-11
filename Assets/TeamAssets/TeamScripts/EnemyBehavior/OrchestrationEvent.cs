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
		StartCoroutine(Orchestrate());
	}
	
	private IEnumerator Orchestrate()
	{
		yield return new WaitForSeconds(beginAfterTime);
		orchestratables.ForEach(obj => obj.SetActive(true));
		yield return new WaitForSeconds(duration);
		orchestratables.ForEach(obj => obj.SetActive(false));
		Destroy(gameObject);
		while (currentState != OrchestrationState.After)
		{
			switch (currentState)
			{
				case OrchestrationState.Start:
					Debug.Log(name + " start orchestration");
					currentState = OrchestrationState.Before;
					yield return new WaitForSeconds(beginAfterTime);
					break;
				case OrchestrationState.Before:
					Debug.Log(name + " begin objs");
					currentState = OrchestrationState.During;
					// start orchestratables
					
					yield return new WaitForSeconds(duration);
					break;
				case OrchestrationState.During:
					// end the things
					Debug.Log(name + " end objs");
					orchestratables.ForEach(obj => obj.SetActive(false));
					Debug.Log("deactived orchestration event");
					gameObject.SetActive(false);
					break;
				case OrchestrationState.After:
					break;
				
			}
		}

	}
}
