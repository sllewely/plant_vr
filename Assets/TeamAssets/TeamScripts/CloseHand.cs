using UnityEngine;

// Tilt the cube using the arrow keys.  When the arrow keys are released
// the cube will be rotated back to the center using Slerp.

public class CloseHand : MonoBehaviour
{
    public GameObject leftSide;
    public GameObject rightSide;
    float smooth = 5.0f;
    float tiltAngle = 60.0f;

    void Update()
    {
        // Smoothly tilts a transform towards a target rotation.
        //float tiltAroundZ = Input.GetAxis("Horizontal") * tiltAngle;
        float tiltAroundY = Input.GetAxis("Vertical") * tiltAngle;

        Quaternion target = Quaternion.Euler(0, tiltAroundY, 0);

        // Dampen towards the target rotation
        leftSide.transform.rotation = Quaternion.Slerp(transform.rotation, target, Time.deltaTime * smooth);
        rightSide.transform.rotation = Quaternion.Slerp(Quaternion.Inverse(transform.rotation), target, Time.deltaTime * smooth);
    }
}