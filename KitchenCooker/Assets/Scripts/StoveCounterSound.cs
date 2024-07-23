using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveCounterSound : MonoBehaviour
{
    [SerializeField] private StoveCounter stoveCounter;

    private AudioSource audioSource;
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }
    private void Start()
    {
        stoveCounter.OnStoveCooking += StoveCounter_OnStoveCooking;
    }

    private void StoveCounter_OnStoveCooking(object sender, StoveCounter.OnStoveCookingEventArgs e)
    {
        //if (e.ToggleOn) audioSource.Play();
        //else audioSource.Stop();
        
        if (e.ToggleOn) {
            audioSource.Play();
        } else {
            audioSource.Pause();
        }
        
    }
}
