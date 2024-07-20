using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveCounterVisual : MonoBehaviour
{
    [SerializeField] private StoveCounter stoveCounter;
    [SerializeField] private GameObject[] fryingVisual;
    private void Start()
    {
        stoveCounter.OnStoveCooking += StoveCounter_OnStoveCookingChanged;
    }

    private void StoveCounter_OnStoveCookingChanged(object sender, StoveCounter.OnStoveCookingEventArgs e)
    {
        if (e.ToggleOn)
        {
            foreach (GameObject frying in fryingVisual)
            {
                frying.SetActive(true);
            }
        }
        else
        {
            foreach (GameObject frying in fryingVisual)
            {
                frying.SetActive(false);
            }
        }
    }
}

