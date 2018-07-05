using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoleBehavior : MonoBehaviour {
    public enum MoleState { Rest, Raise, Lower }
    public MoleState moleState = MoleState.Raise;
    public float verticalSpeed = 0.5f;
    public float timeInterval = 2f;
    public Vector3 startPosition;
    public float randomCircleRadius = 3f;

	// Use this for initialization
	void Start () {
        startPosition = transform.position;
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

    void NewLocation()
    {
        Vector2 randomCirclePos = Random.insideUnitCircle * randomCircleRadius;
        transform.position = startPosition + new Vector3(randomCirclePos.x, 0, randomCirclePos.y);
        Debug.Log("new position is " + transform.position);
    }

    IEnumerator MoleMove()
    {
        for (int i = 0; true; i = (i + 1) % 3)
        {
            moleState = (MoleState)i;
            Debug.Log("mole state is " + moleState);
            if (moleState == MoleState.Rest) {
                NewLocation();
            }
            yield return new WaitForSeconds(timeInterval);
        }

    }
}
