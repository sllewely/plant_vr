using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EatingRegionBehavior : MonoBehaviour {
    private AudioSource omnomnomPlayer;
    private ParticleSystem eatParticles;
    public UnityEvent eat;

    void Start()
    {
        omnomnomPlayer = GetComponent<AudioSource>();
        eatParticles = GetComponentInChildren<ParticleSystem>();
    }
    public void playEatEffects()
    {
        omnomnomPlayer.Play();
        eatParticles.Play();
        eat.Invoke();
    }
}
