using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezeHelper {

    public static FreezeTime GetFreezeScript()
    {
        return GameObject.Find("FreezeManager").GetComponent<FreezeTime>();
    }
}
