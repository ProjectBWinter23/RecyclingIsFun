using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProfilePage : MonoBehaviour
{
    private void OnEnable()
    {
        EventManager.OnScreenChange += OnNotify;
        UIReferences.Instance.takeAPictureBtn.onClick.AddListener(OpenCameraModule);
        
    }

    public void OnNotify(string eventName)
    {
        if (eventName == EventManager.PROFILE_PAGE)
        {
            UIReferences.Instance.profilePage.SetActive(true);
        }
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            UIReferences.Instance.profilePage.SetActive(false);
            EventManager.ChangeScreen(EventManager.HOME_PAGE);
            return;
        }
    }

    private void OpenCameraModule()
    {
        UIReferences.Instance.profilePage.SetActive(false);
        EventManager.ChangeScreen(EventManager.CAMERA_PAGE);
    }

    private void OnDisable()
    {
        EventManager.OnScreenChange -= OnNotify;
        UIReferences.Instance.takeAPictureBtn.onClick.RemoveListener(OpenCameraModule);
    }
}
