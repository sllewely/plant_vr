using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezeHelper {

    public static FreezeTime GetFreezeScript()
    {
        var freezeManager = GameObject.Find("FreezeManager").GetComponent<FreezeTime>();
        if (freezeManager == null)
        {
            Debug.LogError("FreezeManager expected but not found in scene");
        }
        return freezeManager;
    }
}
