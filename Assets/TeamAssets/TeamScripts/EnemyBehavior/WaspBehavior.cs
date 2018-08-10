using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaspBehavior : PreyBehavior {

    // TODO(Sarah): Make pos calculations based on previous frame, not starting pos

    float cycleTime = 0;
    Vector3 startPos;
    Vector3 xNorm = new Vector3(90, 0, 0);
    Vector3 yNorm = new Vector3(0, 90, 0);

    Vector3 lastY = new Vector3(0, 0, 0);
    Vector3 lastX = new Vector3(0, 0, 0);

    public float circleSpeed;
    public float forwardSpeed;
    public float xWaver;
    public float yWaver;

    // Use this for initialization
    public override void Setup() {
        startPos = transform.position;
	}

    public override void Act()
    {
        cycleTime += Time.deltaTime;
        transform.position += Movement();
    }

    private Vector3 XWaver()
    {
        Vector3 nextPoint = Vector3.Normalize((transform.rotation.eulerAngles + xNorm)) * Mathf.Sin(cycleTime * circleSpeed) *xWaver;
        Vector3 nextMove = nextPoint - lastX;
        lastX = nextPoint;
        return nextMove;
    }

    private Vector3 YWaver()
    {
        Vector3 nextPoint = Vector3.Normalize((transform.rotation.eulerAngles + yNorm)) * Mathf.Cos(cycleTime * circleSpeed) * yWaver;
        Vector3 nextMove = nextPoint - lastY;
        lastY = nextPoint;
        return nextMove;
    }

    private Vector3 Movement()
    {
        return (transform.forward * forwardSpeed) + YWaver() + XWaver();
    }
}
