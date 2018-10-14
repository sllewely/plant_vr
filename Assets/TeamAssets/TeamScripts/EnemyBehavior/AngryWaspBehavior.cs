using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class AngryWaspBehavior : PreyBehavior
{

    // TODO(Sarah): Make pos calculations based on previous frame, not starting pos

    enum AngryWaspState
    {
        Spin,
        Pause,
        Attack
    }

    private AngryWaspState angryWaspState;
    private GameObject target;
    private Quaternion destRot;

    public float attackRange;
    public float attackSpeed;

    // Spin variables
    float cycleTime = 0;
    Vector3 startPos;
    Vector3 xNorm = new Vector3(90, 0, 0);
    Vector3 yNorm = new Vector3(0, 90, 0);

    Vector3 lastY = new Vector3(0, 0, 0);
    Vector3 lastX = new Vector3(0, 0, 0);

    public float minCircleSpeed;
    public float maxCircleSpeed;
    float circleSpeed;
    public float forwardSpeed;
    public float minXWaver;
    public float maxXWaver;
    float xWaver;
    public float minYWaver;
    public float maxYWaver;
    float yWaver;

    // Use this for initialization
    public override void Setup()
    {
        startPos = transform.position;
        angryWaspState = AngryWaspState.Spin;
        circleSpeed = Random.Range(minCircleSpeed, maxCircleSpeed);
        xWaver = Random.Range(minXWaver, maxXWaver);
        yWaver = Random.Range(minYWaver, maxYWaver);

        target = PlayerHelper.GetPlayer();
    }

    public override void Act()
    {
        cycleTime += Time.deltaTime;
        switch (angryWaspState)
        {
            case AngryWaspState.Spin:
                transform.position += SpinMovement();
                if (Vector3.Distance(transform.position, target.transform.position) < attackRange)
                {
                    angryWaspState = AngryWaspState.Pause;
                    cycleTime = 0;
                    var targetDir = target.transform.position - transform.position;
                    destRot = Quaternion.LookRotation(targetDir, Vector3.up);
//                    Debug.Log("Wasp pause");
                }

                break;
            case AngryWaspState.Pause:
                // Parameterize pause time
                var rotation = Quaternion.RotateTowards(transform.rotation, destRot, 60 * Time.deltaTime);

                var diff = Quaternion.Angle(destRot, transform.rotation);
                transform.rotation = rotation;
                if (cycleTime > 1 && diff <= 0)
                {
                    angryWaspState = AngryWaspState.Attack;
//                    Debug.Log("wasp attack");
                }

                break;
            case AngryWaspState.Attack:
                Attack();
                break;
        }

    }

    private void Attack()
    {
        var step = attackSpeed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, target.transform.position, step);
    }

    private Vector3 XWaver()
    {
        Vector3 nextPoint = Vector3.Normalize((transform.rotation.eulerAngles + xNorm)) *
                            Mathf.Sin(cycleTime * circleSpeed) * xWaver;
        Vector3 nextMove = nextPoint - lastX;
        lastX = nextPoint;
        return nextMove;
    }

    private Vector3 YWaver()
    {
        Vector3 nextPoint = Vector3.Normalize((transform.rotation.eulerAngles + yNorm)) *
                            Mathf.Cos(cycleTime * circleSpeed) * yWaver;
        Vector3 nextMove = nextPoint - lastY;
        lastY = nextPoint;
        return nextMove;
    }

    private Vector3 SpinMovement()
    {
        return (transform.forward * forwardSpeed) + YWaver() + XWaver();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (angryWaspState == AngryWaspState.Attack && other.tag == "EatingRegion")
        {
            Debug.Log("wasp attacks in trigger enter");
            // Only attack if not held
            Grab grab = GetComponentInParent<Grab>();
            if (grab == null)
            {
                Debug.Log("Attacked by: " + name);
                Destroy(gameObject);
            }
        }
    }
}
