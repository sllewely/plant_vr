using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// A class of static helper methods for the player
public class PlayerHelper {

    public static Vector3 GetPlayerLocation()
    {
        return GameObject.Find("[CameraRig]").transform.position;
    }
}
