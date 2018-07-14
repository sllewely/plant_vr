using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grab : MonoBehaviour {
    public SteamVR_TrackedObject trackedObject;
    public SteamVR_Controller.Device device;
    public bool isHolding = false;
    public float throwSpeed = 2.0f;
    public GameObject grabSpot;
    // Use this for initialization
    void Start () {
        trackedObject = GetComponent<SteamVR_TrackedObject>();
    }
	
	// Update is called once per frame
	void Update () {
        device = SteamVR_Controller.Input((int)trackedObject.index);
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
                isHolding = true;
            }
            if (device.GetPressUp(SteamVR_Controller.ButtonMask.Trigger))
            {
                col.gameObject.GetComponent<Rigidbody>().isKinematic = false;
                col.gameObject.transform.SetParent(null);
                TossObject(col.attachedRigidbody);
                isHolding = false;
            }
        }
    }
    public void TossObject(Rigidbody rigidBody)
    {
        rigidBody.velocity = device.velocity * throwSpeed;
        rigidBody.angularVelocity = device.angularVelocity;
    }
}
