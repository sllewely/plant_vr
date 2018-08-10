using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaspBehavior : PreyBehavior {

    // TODO(Sarah): Make pos calculations based on previous frame, not starting pos

    float cycleTime = 0;
    Vector3 startPos;
    Vector3 xNorm = new Vector3(90, 0, 0);
    Vector3 yNorm = new Vector3(0, 90, 0);


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
        cycleTime += Time.deltaTime;
        transform.position += Movement();
        //
        //var xPos = startPos.x + Mathf.Sin(cycleTime * circleSpeed) * xWaver;
        //var yPos = startPos.y + Mathf.Cos(cycleTime * circleSpeed) * yWaver;
        //var zPos = transform.position.z + (Time.deltaTime * forwardSpeed);
        //transform.position = new Vector3(xPos, yPos, zPos);
    }

    private Vector3 XWaver()
    {
        return (transform.rotation.eulerAngles + xNorm) * Mathf.Sin(cycleTime * circleSpeed) * xWaver;
    }

    private Vector3 YWaver()
    {
        Debug.Log("Cos: " + Mathf.Cos(cycleTime * circleSpeed));
        Debug.Log("Normalize: " + Vector3.Normalize((transform.rotation.eulerAngles + yNorm) * Mathf.Cos(cycleTime * circleSpeed)));
        return Vector3.Normalize((transform.rotation.eulerAngles + yNorm) * Mathf.Cos(cycleTime * circleSpeed)) * yWaver;
    }

    private Vector3 Movement()
    {
        return (transform.forward * forwardSpeed) + YWaver() + XWaver();
    }
}
