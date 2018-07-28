using UnityEngine;

using Valve.VR;

// Tilt the cube using the arrow keys.  When the arrow keys are released
// the cube will be rotated back to the center using Slerp.

public class CloseHandTrigger : MonoBehaviour
{

    public SteamVR_TrackedObject trackedObject;
    public SteamVR_Controller.Device device;
    public GameObject leftSide;
    public GameObject rightSide;
    float smooth = 5.0f;
    float tiltAngle = 45.0f;
    float closeLevel = 0f;

    void Start()
    {
        trackedObject = GetComponent<SteamVR_TrackedObject>();
    }

    void Update()
    {
        device = SteamVR_Controller.Input((int)trackedObject.index);

        if (this.transform.parent.GetComponent<Grab>().isHolding)
        {
            closeLevel = 0f;
        } else {
            closeLevel = device.GetAxis(EVRButtonId.k_EButton_SteamVR_Trigger).x * 45;
        }

        leftSide.transform.localEulerAngles = new Vector3(0, closeLevel, 0);
        rightSide.transform.localEulerAngles = new Vector3(0, -closeLevel, 0);

        //// Raycast object detection
        //Vector3 leftDir = leftSide.transform.TransformDirection(new Vector3(1,0,1));
        //Vector3 rightDir = rightSide.transform.TransformDirection(new Vector3(-1, 0, 1));

        //RaycastHit leftHit;
        //RaycastHit rightHit;

        //Debug.DrawRay(leftSide.GetComponent<Renderer>().bounds.center, leftDir*.1f, Color.yellow, 0.5f);
        //Debug.DrawRay(rightSide.GetComponent<Renderer>().bounds.center, rightDir*.1f, Color.yellow, 0.5f);

        //if (Physics.Raycast(leftSide.GetComponent<Renderer>().bounds.center, leftDir, out leftHit, 0.1f) &&
        //    Physics.Raycast(rightSide.GetComponent<Renderer>().bounds.center, rightDir, out rightHit, 0.1f) &&
        //    leftHit.transform == rightHit.transform)
        //{            
        //        print("There is something in front of the object!");
        //    Debug.DrawRay(leftSide.GetComponent<Renderer>().bounds.center, leftDir * .1f, Color.blue, 0.5f);
        //    Debug.DrawRay(rightSide.GetComponent<Renderer>().bounds.center, rightDir * .1f, Color.blue, 0.5f);
        //}
        //else
        //{
        //    Debug.DrawRay(leftSide.GetComponent<Renderer>().bounds.center, leftDir * .1f, Color.yellow, 0.5f);
        //    Debug.DrawRay(rightSide.GetComponent<Renderer>().bounds.center, rightDir * .1f, Color.yellow, 0.5f);
        //}
    }
}