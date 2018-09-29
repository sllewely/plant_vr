using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// A class of static helper methods for the player
public class PlayerHelper
{
    public static GameObject player;
    
    public static GameObject GetPlayer()
    {
        // TODO: Change to FindWithTag for Performance
        return FindWithWarning("[CameraRig]");
    }

    public static Vector3 GetPlayerLocation(GameObject player)
    {
        return player.transform.position;
    }

    // TODO: Handle only one hand maybe?
    public static GameObject[] GetHands()
    {
        return GameObject.FindGameObjectsWithTag("Hand");
    }

    public static GameObject GetLeftHand()
    {
        return FindWithWarning("[CameraRig]/LeftHand");
    }
    
    public static GameObject GetRightHand()
    {
        return FindWithWarning("[CameraRig]/RightHand");
    }

    // Logs a warning if the object is not found
    private static GameObject FindWithWarning(string name)
    {
        var obj = GameObject.Find(name);
        if (obj == null)
        {
            Debug.LogError("Game object with name " + name + " not found");
        }
        return obj;
    }
}
