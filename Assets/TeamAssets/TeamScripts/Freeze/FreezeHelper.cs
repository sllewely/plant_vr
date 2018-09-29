using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezeHelper {

    public static PlayerFreezeForLaserGopher GetFreezeScript()
    {
        return GameObject.Find("FreezeManager").GetComponent<PlayerFreezeForLaserGopher>();
    }
}
