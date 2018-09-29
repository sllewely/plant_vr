﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezeTime : MonoBehaviour {
	
	// Handle freezing
	private bool freezeTime;
	public float movementBuffer;
	public AudioSource youMovedSound;

	// Hand positions
	private GameObject leftController;
	private GameObject rightController;
	private Vector3 lastLeftPos;
	private Vector3 lastRightPos;

	private void Start ()
	{
		freezeTime = false;
	}

	private void FixedUpdate()
	{
		if (freezeTime)
		{
			DetectMovement();
		}
	}
	
	public void BeginFreezeTime()
	{
		Debug.Log("begin freeze time");
		FetchHands();
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
		Debug.Log("Left hand movement: " + Vector3.Distance(lastLeftPos, newLeftPos));
		if (Vector3.Distance(lastLeftPos, newLeftPos) > movementBuffer)
		{
			return true;
		}
		return Vector3.Distance(lastRightPos, newRightPos) > movementBuffer;
	}

	private void FetchHands()
	{
		if (leftController == null || rightController == null)
		{
			leftController = PlayerHelper.GetLeftHand();
			rightController = PlayerHelper.GetRightHand();
		}
	}
}
