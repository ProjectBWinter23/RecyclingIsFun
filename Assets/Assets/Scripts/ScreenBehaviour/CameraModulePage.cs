using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraModulePage : MonoBehaviour, IObserver
{
    // Start is called before the first frame update
    void OnEnable()
    {
        EventManager.OnScreenChange += OnNotify;
    }

    public void OnNotify(string eventName)
    {
        if (eventName == EventManager.CAMERA_PAGE)
        {
            UIReferences.Instance.cameraPage.SetActive(true);
            UIReferences.Instance.profilePage.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnDisable()
    {
        EventManager.OnScreenChange -= OnNotify;
    }
}
