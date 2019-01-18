using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShroomyBehavior : MonoBehaviour
{
	private Animator anim;

	// Use this for initialization
	void Start ()
	{
		anim = GetComponent<Animator>();

	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.H))
		{
			toWalk();
		}

		if (Input.GetKeyDown(KeyCode.J))
		{
			toStop();
		}

		if (Input.GetKeyDown(KeyCode.K))
		{
			toPickedUp();
		}

		if (Input.GetKeyDown(KeyCode.L))
		{
			toDropped();
		}
	}

	private void toWalk()
	{
		anim.SetBool("StartWalkCond", true);
	}

	private void toStop()
	{
		anim.SetBool("StopWalkCond", true);
	}

	private void toPickedUp()
	{
		anim.SetBool("GrabbedCond", true);
	}

	private void toDropped()
	{
		anim.SetBool("isDropped", true);
	}
}
