using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WrongChoicePage : MonoBehaviour
{
    private void OnEnable()
    {
        EventManager.OnScreenChange += OnNotify;
        UIReferences.Instance.quitWrongBtn.onClick.AddListener(QuitGame);
        UIReferences.Instance.continueWrongBtn.onClick.AddListener(ContinueGame);
    }

    public void OnNotify(string eventName)
    {
        if (eventName == EventManager.WRONG_CHOICE_PAGE)
        {
            UIReferences.Instance.wrongChoicePage.SetActive(true);
        }
    }

    private void QuitGame()
    {
#if UNITY_EDITOR
        // Quitting the game in the Unity Editor
        UnityEditor.EditorApplication.isPlaying = false;
#else
        // Quitting the game on an Android device
        Application.Quit();
#endif
    }

    private void ContinueGame()
    {
        UIReferences.Instance.wrongChoicePage.SetActive(false);
        EventManager.ChangeScreen(EventManager.PROFILE_PAGE);
    }

    private void OnDisable()
    {
        EventManager.OnScreenChange -= OnNotify;
        UIReferences.Instance.quitWrongBtn.onClick.RemoveListener(QuitGame);
        UIReferences.Instance.continueWrongBtn.onClick.RemoveListener(ContinueGame);
    }
}
