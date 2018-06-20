using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedLightGreenLight : MonoBehaviour
{
    // TODO: Also freeze button pushes?
    // TODO: Read up on relative positioning 
    //       - does a constant value for distanceCheck work?
    // TODO: Freeze HMD movement as well?
    // TODO: On-screen indicator when you move during red light, 
    //       and/or indicator that you successfully froze after red light
    public GameObject leftController;
    public GameObject rightController;
    private Vector3 lastLeftPosition;
    private Vector3 lastRightPosition;
    //public GameObject hmd;
    public GameObject stoplight;
    private Renderer stoplightrend;
    private LinkedList<Color> lights;
    private LinkedListNode<Color> currentLight;
    private bool move = true;

    //private Vector3 lastposition;
    public float checkTime = 0.5f;
    public float distanceCheck = 0.005f; // set to checkTime/100?
    public float lightTime = 3f;
    public int score = 100;

    // Use this for initialization
    void Start()
    {
        lastLeftPosition = leftController.transform.position;
        lastRightPosition = rightController.transform.position;
        stoplightrend = stoplight.GetComponent<Renderer>();
        Color[] light_colors = { Color.green, Color.yellow, Color.red };
        lights = new LinkedList<Color>(light_colors);
        currentLight = lights.First;
        StartCoroutine(GoStop(stoplightrend));
        StartCoroutine(MoveCheck());
    }

    // Update is called once per frame
    void Update()
    {

    }

    // Reduce score if right or left controller has changed position during a red light
    IEnumerator MoveCheck()
    {
        while (true)
        {
            float leftMovement = Vector3.Distance(lastLeftPosition, leftController.transform.position);
            float rightMovement = Vector3.Distance(lastRightPosition, rightController.transform.position);
            //Debug.Log(move.ToString() + "," + distanceCheck.ToString() + ","
            //    +leftMovement.ToString() + ":" + (leftMovement > distanceCheck).ToString() + "," 
            //    +rightMovement.ToString() + ":" + (rightMovement > distanceCheck).ToString());

            if (!move && (leftMovement > distanceCheck || rightMovement > distanceCheck))
            {
                score--;
                Debug.Log("You moved. Score: " + score.ToString());
            }
            lastLeftPosition = leftController.transform.position;
            lastRightPosition = rightController.transform.position;
            yield return new WaitForSeconds(checkTime);
        }
    }

    // Cycle through green/yellow/red lights
    IEnumerator GoStop(Renderer r)
    {
        while (true)
        {
            currentLight = currentLight.Next ?? currentLight.List.First; // circular
            r.material.SetColor("_Color", currentLight.Value);
            move = currentLight != lights.Last; // allow movement if not red
            yield return new WaitForSeconds(lightTime);
        }
    }
}