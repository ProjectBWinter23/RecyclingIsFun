using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeChoicePage : MonoBehaviour,IObserver
{
    // Adjust this value to set the delay 
    private float delay = 5f;

    private void Start()
    {
    }
    private void OnEnable()
    {
        EventManager.OnScreenChange += OnNotify;
    }

    public void OnNotify(string eventName)
    {
        if (eventName == EventManager.MAKE_CHOICE_PAGE)
        {
            UIReferences.Instance.makeYourChoicePage.SetActive(true);
            Invoke("ShowChoicesPage", delay);
        }
    }

    private void ShowChoicesPage()
    {
        UIReferences.Instance.makeYourChoicePage.SetActive(false);
        EventManager.ChangeScreen(EventManager.CHOOSE_CORRECT_PAGE);
    }

    private void OnDisable()
    {
        EventManager.OnScreenChange -= OnNotify;
    }
}
