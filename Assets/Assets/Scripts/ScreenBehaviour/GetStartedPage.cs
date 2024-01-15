using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetStartedPage : MonoBehaviour, IObserver
{
    // Adjust this value to set the delay for the splash screen
    private float splashScreenDelay = 2f;

    private void Start()
    {
        UIReferences.Instance.splashScreen.SetActive(true);
        Invoke("ShowGetStarted", splashScreenDelay);
    }
    private void OnEnable()
    {
        EventManager.OnScreenChange += OnNotify;
        UIReferences.Instance.getStartedBtn.onClick.AddListener(OpenHomePage);
    }

    public void OnNotify(string eventName)
    {
       
    }

    public void ShowGetStarted()
    {
        UIReferences.Instance.getStartedPage.SetActive(true);
        UIReferences.Instance.splashScreen.SetActive(false);
    }

    public void OpenHomePage()
    {
        UIReferences.Instance.getStartedPage.SetActive(false);
        EventManager.ChangeScreen(EventManager.HOME_PAGE);
    }

    private void OnDisable()
    {
        EventManager.OnScreenChange -= OnNotify;
        UIReferences.Instance.getStartedBtn.onClick.RemoveListener(OpenHomePage);
    }
}
