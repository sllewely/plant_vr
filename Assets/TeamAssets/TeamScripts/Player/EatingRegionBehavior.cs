using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EatingRegionBehavior : MonoBehaviour {
    private AudioSource omnomnomPlayer;
    private ParticleSystem eatParticles;

    void Start()
    {
        omnomnomPlayer = GetComponent<AudioSource>();
        eatParticles = GetComponentInChildren<ParticleSystem>();
    }
    public void playEatEffects()
    {
        omnomnomPlayer.Play();
        eatParticles.Play();
    }
}
