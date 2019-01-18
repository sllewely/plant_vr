using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShroomyBehavior : MonoBehaviour
{
	private Animator anim;
	private int toIdleHash = Animator.StringToHash("toIdle");
	private int toWalkHash = Animator.StringToHash("toWalk");
	private int toGrabbedHash = Animator.StringToHash("toGrabbed");

	// Use this for initialization
	void Start ()
	{
		anim = GetComponent<Animator>();

	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.J))
		{
			toWalk();
		}

		if (Input.GetKeyDown(KeyCode.K))
		{
			toIdle();
		}

		if (Input.GetKeyDown(KeyCode.L))
		{
			toGrabbed();
		}

	}

	private void toWalk()
	{
		anim.SetTrigger(toWalkHash);
	}

	private void toIdle()
	{
		anim.SetTrigger(toIdleHash);
	}

	private void toGrabbed()
	{
		anim.SetTrigger(toGrabbedHash);
	}
}
