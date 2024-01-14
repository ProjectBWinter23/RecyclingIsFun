using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CameraOff_EventHandler : MonoBehaviour
{

    [Header("Events")]
    public GameEvent OnCameraOffPressed;

    public void Invoke()
    {
        OnCameraOffPressed.Raise();
    }
}
