using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorrectChoicePage : MonoBehaviour
{
    private void OnEnable()
    {
        EventManager.OnScreenChange += OnNotify;
        UIReferences.Instance.quitCorrectBtn.onClick.AddListener(QuitGame);
        UIReferences.Instance.continueCorrectBtn.onClick.AddListener(ContinueGame);
    }

    public void OnNotify(string eventName)
    {
        if (eventName == EventManager.CORRECT_CHOICE_PAGE)
        {
            UIReferences.Instance.correctChoicePage.SetActive(true);
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
        UIReferences.Instance.correctChoicePage.SetActive(false);
        EventManager.ChangeScreen(EventManager.PROFILE_PAGE);
    }

    private void OnDisable()
    {
        EventManager.OnScreenChange -= OnNotify;
        UIReferences.Instance.quitCorrectBtn.onClick.RemoveListener(QuitGame);
        UIReferences.Instance.continueCorrectBtn.onClick.RemoveListener(ContinueGame);
    }
}
