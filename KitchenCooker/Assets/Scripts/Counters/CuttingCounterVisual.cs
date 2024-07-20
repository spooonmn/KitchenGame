using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingCounterVisual : MonoBehaviour
{
    private const string CUT = "Cut";

    [SerializeField] private CuttingCounter CuttingCounter;
    private Animator animator;
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    private void Start()
    {
        CuttingCounter.OnCut += ContainerCounter_OnPlayerCutObject;
    }

    private void ContainerCounter_OnPlayerCutObject(object sender, EventArgs e)
    {
        animator.SetTrigger(CUT);
    }
}
