using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaspBehavior : MonoBehaviour {

    // TODO(Sarah): Make pos calculations based on previous frame, not starting pos

    float cycleTime = 0;
    Vector3 startPos;

    public float circleSpeed;
    public float forwardSpeed;
    public float circleSize = 1;
    // var circleGrowSpeed = 0.1;


    // var xPos = Mathf.Sin(Time.time * circleSpeed) * circleSize;
    //  var yPos = Mathf.Cos(Time.time * circleSpeed) * circleSize;
    // var zPos += forwardSpeed* Time.deltaTime;

    // circleSize += circleGrowSpeed;

	// Use this for initialization
	void Start () {
        startPos = transform.position;
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void FixedUpdate()
    {
        Move();
    }

    void Move()
    {
        cycleTime += Time.deltaTime;
        var xPos = startPos.x + Mathf.Sin(cycleTime * circleSpeed) * circleSize;
        var yPos = startPos.y + Mathf.Cos(cycleTime * circleSpeed) * circleSize;
        var zPos = transform.position.z + (Time.deltaTime * forwardSpeed);
        transform.position = new Vector3(xPos, yPos, zPos);
    }
}
