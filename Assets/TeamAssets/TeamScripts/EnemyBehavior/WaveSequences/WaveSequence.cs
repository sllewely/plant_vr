using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WaveSequence : MonoBehaviour
{

    // todo
    public int offsetTime;
    public List<WaveEvent> waveEvents = new List<WaveEvent>();
    
    public WaveSequence()
    {
        Init();
    }

    protected abstract void Init();
}
