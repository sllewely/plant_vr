using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;


public class FlytrapAnimatorBlendshapes : MonoBehaviour {

    private SteamVR_Controller.Device device;
    private SteamVR_TrackedObject trackedObject;
    private SkinnedMeshRenderer skinnedMeshRenderer;
    private Mesh skinnedMesh;
    //public Animator armAnimator;
    //private ushort pulsePower = 700;
    private ulong grabButton = SteamVR_Controller.ButtonMask.Trigger;
    private float blendLevel;

    void Awake()
    {
        trackedObject = GetComponentInParent<SteamVR_TrackedObject>();
        skinnedMeshRenderer = GetComponent<SkinnedMeshRenderer>();
        skinnedMesh = GetComponent<SkinnedMeshRenderer>().sharedMesh;
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update () {
        // TODO: abstracted inputs
        device = SteamVR_Controller.Input((int)trackedObject.index);

        // Get trigger pull amount and map it to blendshape
        if (this.transform.parent.GetComponent<Grab>().isHolding)
        {
            skinnedMeshRenderer.SetBlendShapeWeight(0, 50);
        } else
        {
            blendLevel = device.GetAxis(EVRButtonId.k_EButton_SteamVR_Trigger).x * 100;
            skinnedMeshRenderer.SetBlendShapeWeight(0, blendLevel);
        }


        // Issues with below: the PressDown vs PressUp seems to happen at a halfway trigger pull
        // and it caused the blendshape to briefly flash open/closed
        // GetPress also caused blendshape to jump to halfway

        //if (device.GetPressDown(grabButton))
        //    blendLevel = 100f;
        //else if (device.GetPressUp(grabButton))
        //    blendLevel = 0f;
        //else if (device.GetPress(grabButton))
        //    blendLevel = device.GetAxis(EVRButtonId.k_EButton_SteamVR_Trigger).x * 100;


        // Activate open/close animation based on button press/release
        // Updated to blendshape above

        //if (device.GetPress(grabButton))
        //{
        //    device.TriggerHapticPulse(pulsePower);
        //    armAnimator.SetBool("isClosed", true);
        //}
        //else if (device.GetPressUp(grabButton))
        //{
        //    armAnimator.SetBool("isClosed", false);
        //}

        //if (device.GetPress(grabButton))
        //{
        //    device.TriggerHapticPulse(pulsePower);
        //}
    }
}
