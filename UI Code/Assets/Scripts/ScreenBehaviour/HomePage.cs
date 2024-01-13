using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomePage : MonoBehaviour, IObserver
{
    private void OnEnable()
    {
        EventManager.OnScreenChange += OnNotify;
        UIReferences.Instance.playGameBtn.onClick.AddListener(PlayGame);
        UIReferences.Instance.learnRecyclingBtn.onClick.AddListener(LearnRecycling);
        UIReferences.Instance.aboutBtn.onClick.AddListener(OpenAbout);
    }

    public void OnNotify(string eventName)
    {
        if (eventName == EventManager.HOME_PAGE)
        {
            UIReferences.Instance.homePage.SetActive(true);
        }
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            UIReferences.Instance.homePage.SetActive(false);
            EventManager.ChangeScreen(EventManager.GET_STARTED);
            return;
        }
    }

        public void PlayGame()
    {
        UIReferences.Instance.homePage.SetActive(false);
        EventManager.ChangeScreen(EventManager.PROFILE_PAGE);
    }

    public void LearnRecycling()
    {
        UIReferences.Instance.homePage.SetActive(false);
        EventManager.ChangeScreen(EventManager.LEARN_PAGE);
    }

    public void OpenAbout()
    {
        UIReferences.Instance.homePage.SetActive(false);
        EventManager.ChangeScreen(EventManager.ABOUT_PAGE);
    }

    private void OnDisable()
    {
        EventManager.OnScreenChange -= OnNotify;
        UIReferences.Instance.playGameBtn.onClick.RemoveListener(PlayGame);
        UIReferences.Instance.learnRecyclingBtn.onClick.RemoveListener(LearnRecycling);
        UIReferences.Instance.aboutBtn.onClick.RemoveListener(PlayGame);
    }
}
