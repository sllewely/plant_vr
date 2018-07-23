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

        float closeLevel = device.GetAxis(EVRButtonId.k_EButton_SteamVR_Trigger).x * 45;

        leftSide.transform.localEulerAngles = new Vector3(0, closeLevel, 0);
        rightSide.transform.localEulerAngles = new Vector3(0, -closeLevel, 0);

    }


}