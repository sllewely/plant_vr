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
    Quaternion rotateRight = Quaternion.Euler(new Vector3(0, 50, 0));
    Quaternion rotateLeft = Quaternion.Euler(new Vector3(0, -50, 0));
    Vector3 fromPos;
    Vector3 toPos;
    Quaternion fromRot;
    Quaternion toRot;

    // Use this for initialization
    void Start () {
        dart = false;
        dartRight = true;
        timeCount = 0;
        fromRot = rotateLeft;
        toRot = rotateRight;
        fromPos = transform.position;
        toPos = Vector3.forward * speed;
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
        transform.position = Vector3.Slerp(fromPos, toPos, timeCount / dartTime);
    }

    void Rotate()
    {
        transform.rotation = Quaternion.Slerp(fromRot, toRot, timeCount/pauseTime);
    }

    void SetToDart()
    {
        dart = true;
        fromPos = transform.position;
        toPos = (transform.forward * speed);
        timeCount = 0;
    }

    void SetToRotate()
    {
        dart = false;
        dartRight = !dartRight;
        fromRot = Quaternion.Euler(Vector3.forward);
        toRot = dartRight ? rotateRight : rotateLeft;
        timeCount = 0;
    }
}
