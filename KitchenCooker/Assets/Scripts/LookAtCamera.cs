using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    private enum Mode { 
        LookAt,
        LookAtInverted,
        CameraForward,
        CameraForwardInverted

    }
    [SerializeField] Mode mode;
    private void LateUpdate()
    {
        switch (mode)
        {

           case Mode.LookAt:
                LookAt();
                break;
            case Mode.LookAtInverted:
                LookAtInverted();
                break;
            case Mode.CameraForward:
                CameraForward();
                break;
            case Mode.CameraForwardInverted:
                CameraForwardInverted();
                break;
        }
       
    }

    private void CameraForwardInverted()
    {
        transform.forward = -Camera.main.transform.forward;
    }

    private void CameraForward()
    {
        transform.forward = Camera.main.transform.forward;
    }

    private void LookAtInverted()
    {
        Vector3 dirFromCamera = transform.position - Camera.main.transform.position;
        transform.LookAt(transform.position + dirFromCamera);
    }

    private void LookAt()
    {
        transform.LookAt(Camera.main.transform);
    }
}
