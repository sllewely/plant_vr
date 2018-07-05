using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoleBehavior : MonoBehaviour {
    public enum MoleState { Rest, Raise, Lower }
    public MoleState moleState = MoleState.Raise;
    public float verticalSpeed = 0.4f;
    public float timeInterval = 3f;

	// Use this for initialization
	void Start () {
        StartCoroutine(MoleMove());
		
	}
	
	// Update is called once per frame
	void Update () {
        switch (moleState)
        {
            case MoleState.Rest:
                break;
            case MoleState.Raise:
                transform.position = transform.position + (new Vector3(0, verticalSpeed, 0) * Time.deltaTime);
                break;
            case MoleState.Lower:
                transform.position = transform.position + (new Vector3(0, -verticalSpeed, 0) * Time.deltaTime);
                break;
        }
    }

    void FixedUpdate()
    {

    }

    IEnumerator MoleMove()
    {
        for (int i = 0; true; i = (i + 1) % 3)
        {
            moleState = (MoleState) (i + 1);
            yield return new WaitForSeconds(timeInterval);
        }

    }
}
