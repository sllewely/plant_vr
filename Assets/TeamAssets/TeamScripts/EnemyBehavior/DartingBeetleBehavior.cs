using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class DartingBeetleBehavior : MonoBehaviour {

    public float speed;
    public float dartTime;
    public float pauseTime;
    float timeCount;
    bool dart;
    bool dartRight;
    Vector3 rotateRight = new Vector3(0, 50, 0);
    Vector3 rotateLeft = new Vector3(0, -50, 0);
    Vector3 fromPos;
    Vector3 toPos;
    Vector3 fromRot;
    Vector3 toRot;

    // Use this for initialization
    void Start () {
        SetToDart();
	}
	
	// Update is called once per frame
	void Update () {
        timeCount = timeCount + Time.deltaTime;
        if (dart)
        {
            Dart();
            if (timeCount > dartTime)
            {
                SetToRotate();
            }
        } else
        {
            Rotate();
            if (timeCount > pauseTime)
            {
                SetToDart();
            }
        }
    }

    void FixedUpdate()
    {

    }

    void Dart()
    {
        transform.position = Vector3.Lerp(fromPos, toPos, timeCount / dartTime);
    }

    void Rotate()
    {
        transform.rotation = Quaternion.Lerp(Quaternion.Euler(fromRot), Quaternion.Euler(toRot), timeCount/pauseTime);
    }

    void SetToDart()
    {
        dart = true;
        fromPos = transform.position;
        toPos = transform.position + (transform.forward * speed);
        timeCount = 0;
    }

    void SetToRotate()
    {
        dart = false;
        dartRight = !dartRight;
        fromRot = transform.eulerAngles;
        toRot = transform.eulerAngles + (dartRight ? rotateRight : rotateLeft);
        timeCount = 0;
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("beetle collided");
    }
}
