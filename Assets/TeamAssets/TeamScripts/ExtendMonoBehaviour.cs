using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtendMonoBehaviour : MonoBehaviour {

	public void InvokeAction(Action action, float time)
	{
		Invoke(action.Method.Name, time);
	}
}
