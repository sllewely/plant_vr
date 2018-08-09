using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaspBehavior : PreyBehavior {

    // TODO(Sarah): Make pos calculations based on previous frame, not starting pos

    float cycleTime = 0;
    Vector3 startPos;


    public float circleSpeed;
    public float forwardSpeed;
    public float xWaver;
    public float yWaver;
    //public float circleSize = 1;
    // var circleGrowSpeed = 0.1;


    // var xPos = Mathf.Sin(Time.time * circleSpeed) * circleSize;
    //  var yPos = Mathf.Cos(Time.time * circleSpeed) * circleSize;
    // var zPos += forwardSpeed* Time.deltaTime;

    // circleSize += circleGrowSpeed;

    // Use this for initialization
    public override void Setup() {
        startPos = transform.position;
	}

    public override void Act()
    {
        transform.position += transform.forward * forwardSpeed;
        //cycleTime += Time.deltaTime;
        //var xPos = startPos.x + Mathf.Sin(cycleTime * circleSpeed) * xWaver;
        //var yPos = startPos.y + Mathf.Cos(cycleTime * circleSpeed) * yWaver;
        //var zPos = transform.position.z + (Time.deltaTime * forwardSpeed);
        //transform.position = new Vector3(xPos, yPos, zPos);
    }
}
