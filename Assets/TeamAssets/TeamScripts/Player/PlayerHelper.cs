using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// A class of static helper methods for the player
public class PlayerHelper
{
    private const string playerNameVr = "[CameraRig]";
    private const string playerNameVrless = "[CameraRigNoVrTest]";
    private static string playerName = playerNameVr;
    
    public static GameObject GetPlayer()
    {
        // TODO: Change to FindWithTag for Performance
        return FindWithWarning(playerName);
    }

    public static GameObject GetLeftHand()
    {
        return FindWithWarning(playerName + "/LeftHand");
    }
    
    public static GameObject GetRightHand()
    {
        return FindWithWarning(playerName + "/RightHand");
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
