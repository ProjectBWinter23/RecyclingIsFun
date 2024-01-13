using UnityEngine.UI;
using UnityEngine;
using System.Collections.Generic;

public class UIReferences : Singleton<UIReferences>
{
    [Header("Screens")]
    public GameObject splashScreen;
    public GameObject getStartedPage;
    public GameObject homePage;
    public GameObject profilePage;
    public GameObject cameraPage;
    public GameObject makeYourChoicePage;
    public GameObject chooseCorrectPage;
    public GameObject wrongChoicePage;
    public GameObject correctChoicePage;
    public GameObject learnRecyclingPage;
    public GameObject aboutPage;

    [Header("Get Started Page")]
    public Button getStartedBtn;

    [Header("Home Page")]
    public Button playGameBtn;
    public Button learnRecyclingBtn;
    public Button aboutBtn;

    [Header("Profile Page")]
    public Button takeAPictureBtn;


    [Header("Choose Answer Page")]
    public Button blueBtn;
    public Button yellowBtn;
    public Button brownBtn;
    public Button greenBtn;
    public Button greyBtn;

    [Header("Wrong Choice Page")]
    public Button quitWrongBtn;
    public Button continueWrongBtn;

    [Header("Correct Choice Page")]
    public Button quitCorrectBtn;
    public Button continueCorrectBtn;

    private List<Button> answerButtons;

    private void Awake()
    {
        // Initialize the answer buttons list
        answerButtons = new List<Button>
        {
            blueBtn,
            yellowBtn,
            brownBtn,
            greenBtn,
            greyBtn
        };
    }

    // Method to get the answer buttons
    public List<Button> GetAnswerButtons()
    {
        return answerButtons;
    }
}
