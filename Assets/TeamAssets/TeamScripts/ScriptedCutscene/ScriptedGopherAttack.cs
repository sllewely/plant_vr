using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptedGopherAttack : MonoBehaviour
{

	public GameObject attackedBunny;
	public GameObject gopher;
	
	Vector3 jumpVector = new Vector3(50f, 100f, 0);

	private int action;
	
	

	// Use this for initialization
	void Start ()
	{
		action = 0;
		StartCoroutine(CutSceneSequence());
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	IEnumerator CutSceneSequence()
	{
		for (; ; )
		{
			switch (action)
			{
				case 0:
					// hop
				case 1:
					// hop a different direction
				case 2:
					// hop a different direction
				default:
					break;
			}

			yield return new WaitForSeconds(0.5f);
		}
	}
	
	
}
