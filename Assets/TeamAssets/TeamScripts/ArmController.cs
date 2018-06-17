using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmController : MonoBehaviour {

    private SteamVR_TrackedObject trackedObject;
    private SteamVR_Controller.Device device;
    public Animator armAnimator;
    private ushort pulsePower = 700;
    private ulong gripButton = SteamVR_Controller.ButtonMask.Grip;
    void Start()
    {
        trackedObject = GetComponent<SteamVR_TrackedObject>();
    }
    // Update is called once per frame
    void Update () {
        device = SteamVR_Controller.Input((int)trackedObject.index);


        if (device.GetPress(gripButton))
        {
            device.TriggerHapticPulse(pulsePower);
            armAnimator.SetBool("isClosed", true);
        }
        else if (device.GetPressUp(gripButton))
        {
            armAnimator.SetBool("isClosed", false);
        }

        //if (device.GetPress(gripButton))
        //{
        //    device.TriggerHapticPulse(pulsePower);
        //}
    }
}
