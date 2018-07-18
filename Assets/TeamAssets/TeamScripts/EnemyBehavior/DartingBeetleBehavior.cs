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
    Quaternion from;
    Quaternion to;

    // Use this for initialization
    void Start () {
        dart = false;
        dartRight = true;
        timeCount = 0;
        from = rotateLeft;
        to = rotateRight;
	}
	
	// Update is called once per frame
	void Update () {
        Rotate();
	}

    void FixedUpdate()
    {

    }

    void Rotate()
    {
        transform.rotation = Quaternion.Slerp(from, to, timeCount/pauseTime);
        timeCount = timeCount + Time.deltaTime;
    }
}
