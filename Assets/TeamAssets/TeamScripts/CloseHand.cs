using UnityEngine;

// Tilt the cube using the arrow keys.  When the arrow keys are released
// the cube will be rotated back to the center using Slerp.

public class CloseHand : MonoBehaviour
{
    public GameObject leftSide;
    public GameObject rightSide;
    float smooth = 5.0f;
    float tiltAngle = 45.0f;

    void Update()
    {
        // Smoothly tilts a transform towards a target rotation.
        //float tiltAroundZ = Input.GetAxis("Horizontal") * tiltAngle;
        float tiltAroundY = Input.GetAxis("Vertical") * tiltAngle;

        Quaternion rtarget = Quaternion.Euler(0, tiltAroundY, 0);
        Quaternion ltarget = Quaternion.Euler(0, -tiltAroundY, 0);
        // Dampen towards the target rotation
        leftSide.transform.rotation = Quaternion.Slerp(leftSide.transform.rotation, rtarget, Time.deltaTime * smooth);
        rightSide.transform.rotation = Quaternion.Slerp(rightSide.transform.rotation, ltarget, Time.deltaTime * smooth);
    }
}