using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grab : MonoBehaviour {
    public SteamVR_TrackedObject trackedObject;
    public SteamVR_Controller.Device device;
    public bool isHolding = false;
    public float throwSpeed = 4.0f;
    public GameObject grabSpot;
    public GameObject heldObject;
    // Use this for initialization
    void Start () {
        trackedObject = GetComponent<SteamVR_TrackedObject>();
    }
	
	// Update is called once per frame
	void Update () {
        device = SteamVR_Controller.Input((int)trackedObject.index);
        if (isHolding)
        {
            device.TriggerHapticPulse(700);
        }
    }

    void OnTriggerStay(Collider col)
    {
        if (col.tag == "Prey")
        {
            if (device.GetPressDown(SteamVR_Controller.ButtonMask.Trigger))
            {
                col.gameObject.GetComponent<Rigidbody>().isKinematic = true;
                col.gameObject.transform.SetParent(gameObject.transform);
                Collider boxcol = this.GetComponent<BoxCollider>();
                col.gameObject.transform.position = grabSpot.transform.position;
                col.gameObject.transform.rotation = grabSpot.transform.rotation;
                isHolding = true;
                col.gameObject.GetComponent<MonoBehaviour>().enabled = false;
                //TODO: get below working--- turn off PreyBehavior scripts only
                //PreyBehavior preyAI = col.gameObject.GetComponent<PreyBehaviour>();
                //preyAI.enabled = false;
                heldObject = col.gameObject;
            }
            if (device.GetPressUp(SteamVR_Controller.ButtonMask.Trigger))
            {
                //TODO: turn script back on 
                //OR turn off isKinematic, turn on gravity
                col.gameObject.GetComponent<Rigidbody>().isKinematic = false;
                col.gameObject.transform.SetParent(null);
                TossObject(col.attachedRigidbody);
                isHolding = false;
                //TODO: turn off isHolding if prey "expires"
                heldObject = heldObject = null;
            }
        }
    }
    public void TossObject(Rigidbody rigidBody)
    {
        rigidBody.velocity = device.velocity * throwSpeed;
        rigidBody.angularVelocity = device.angularVelocity;
    }
}
