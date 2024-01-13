using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Capture_EventHandler : MonoBehaviour
{
    [Header("Events")]
    public GameEvent OnCapturePressed;

    public void Invoke()
    {
        OnCapturePressed.Raise();
    }
}
