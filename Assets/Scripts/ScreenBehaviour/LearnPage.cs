using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LearnPage : MonoBehaviour, IObserver
{
    private void OnEnable()
    {
        EventManager.OnScreenChange += OnNotify;
    }

    public void OnNotify(string eventName)
    {
        if (eventName == EventManager.LEARN_PAGE)
        {
            UIReferences.Instance.learnRecyclingPage.SetActive(true);
        }
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            UIReferences.Instance.learnRecyclingPage.SetActive(false);
            EventManager.ChangeScreen(EventManager.HOME_PAGE);
            return;
        }
    }


    private void OnDisable()
    {
        EventManager.OnScreenChange -= OnNotify;
    }
}
