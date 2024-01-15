using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AboutPage : MonoBehaviour, IObserver
{
    private void OnEnable()
    {
        EventManager.OnScreenChange += OnNotify;
    }

    public void OnNotify(string eventName)
    {
        if (eventName == EventManager.ABOUT_PAGE)
        {
            UIReferences.Instance.aboutPage.SetActive(true);
        }
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            UIReferences.Instance.aboutPage.SetActive(false);
            EventManager.ChangeScreen(EventManager.HOME_PAGE);
            return;
        }
    }


    private void OnDisable()
    {
        EventManager.OnScreenChange -= OnNotify;
    }
}
