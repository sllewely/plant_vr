using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFreezeForLaserGopher : MonoBehaviour {
	
	public float movementBuffer;
//	private GameObject player;
	private GameObject leftController;
	private GameObject rightController;
	public AudioSource youMovedSound;
	private Vector3 lastLeftPos;
	private Vector3 lastRightPos;


	private bool freezeTime;

	private GameObject[] hands;

	// Use this for initialization
	private void Start ()
	{
		freezeTime = false;
//		player = PlayerHelper.GetPlayer();
		leftController = PlayerHelper.GetLeftHand();
		rightController = PlayerHelper.GetRightHand();
//		leftController = GameObject.Find("LeftHand");
//		rightController = GameObject.Find("RightHand");
	}

	private void FixedUpate()
	{
		if (freezeTime)
		{
			DetectMovement();
		}
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

	private void DetectMovement()
	{
		var newLeftPos = leftController.transform.position;
		var newRightPos = rightController.transform.position;
		if (MovedTooMuch(newLeftPos, newRightPos))
		{
			Debug.Log("You moved!");
			youMovedSound.Play();
		}
		lastLeftPos = newLeftPos;
		lastRightPos = newRightPos;
	}

	private bool MovedTooMuch(Vector3 newLeftPos, Vector3 newRightPos)
	{
		if (Vector3.Distance(lastLeftPos, newLeftPos) > movementBuffer)
		{
			return true;
		}
		return Vector3.Distance(lastRightPos, newRightPos) > movementBuffer;
	}
}
