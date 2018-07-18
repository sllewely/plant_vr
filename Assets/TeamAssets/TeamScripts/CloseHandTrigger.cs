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

    void Start()
    {
        trackedObject = GetComponent<SteamVR_TrackedObject>();
    }
    void Update()
    {
        device = SteamVR_Controller.Input((int)trackedObject.index);
        // Smoothly tilts a transform towards a target rotation.
        //float tiltAroundZ = Input.GetAxis("Horizontal") * tiltAngle;
        float closeLevel = device.GetAxis(EVRButtonId.k_EButton_SteamVR_Trigger).x * 45;

        //float tiltAroundY = Input.GetAxis("Vertical") * tiltAngle;

        //Quaternion rtarget = Quaternion.Euler(0, closeLevel, 0);
        //Quaternion ltarget = Quaternion.Euler(0, -closeLevel, 0);
        // Dampen towards the target rotation

        leftSide.transform.eulerAngles = new Vector3(
    leftSide.transform.eulerAngles.x,
    leftSide.transform.eulerAngles.y - closeLevel,
    leftSide.transform.eulerAngles.z
);

        rightSide.transform.eulerAngles = new Vector3(
    rightSide.transform.eulerAngles.x,
    rightSide.transform.eulerAngles.y + closeLevel,
    rightSide.transform.eulerAngles.z
);
        //leftSide.transform.rotation = Quaternion.Slerp(leftSide.transform.rotation, rtarget, Time.deltaTime * smooth);
        //rightSide.transform.rotation = Quaternion.Slerp(rightSide.transform.rotation, ltarget, Time.deltaTime * smooth);
    }


}