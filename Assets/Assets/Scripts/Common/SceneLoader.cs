using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void LoadPlayScene()
    {
        SceneManager.LoadScene(1);
    }

    public void LoadMainMenuScene()
    {
        SceneManager.LoadScene(0);
    }
}