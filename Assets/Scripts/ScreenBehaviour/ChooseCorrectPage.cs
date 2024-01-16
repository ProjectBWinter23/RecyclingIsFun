using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChooseCorrectPage : MonoBehaviour, IObserver
{
    private List<Button> answerButtons = new List<Button>();
    public Color correctColor = Color.green;
    public Color wrongColor = Color.red;
    public Color selectedColor = Color.blue;
    private Color originalColor;
    public float delayTime = 2f;

    private bool pageEnabled = false;
    private Button clickedButton;  // Declare the clickedButton variable


    private void Start()
    {
        answerButtons = UIReferences.Instance.GetAnswerButtons();
        originalColor = answerButtons[0].GetComponent<Image>().color;
    }
    private void OnEnable()
    {
        EventManager.OnScreenChange += OnNotify;

        // Check if the page is enabled
        if (pageEnabled)
        {
            // If enabled, set up the buttons as they were before
            SetupButtons();
        }
    }

    private void OnDisable()
    {
        // Preserve the state of the page when disabled
        EventManager.OnScreenChange -= OnNotify;
        pageEnabled = true;
    }

    public void OnNotify(string eventName)
    {
        if (eventName == EventManager.CHOOSE_CORRECT_PAGE)
        {
            UIReferences.Instance.chooseCorrectPage.SetActive(true);

            // Example: Get the answerButtons collection from another script and cache them
            
            // Attach the onClick listener to all answer buttons
            foreach (Button button in answerButtons)
            {
                button.onClick.AddListener(() => OnButtonClick(button));
            }

            // If the page is enabled, set up the buttons as they were before
            if (pageEnabled)
            {
                SetupButtons();
            }
        }
    }

    private void OnButtonClick(Button button)
    {
        // Disable buttons temporarily to prevent multiple clicks during the delay
        DisableButtons();

        // Change the color of the clicked button to blue
        ChangeColor(button, selectedColor);

        // Set the clickedButton variable
        clickedButton = button;

        // Start a coroutine to wait for a delay before revealing the correct answer
        StartCoroutine(ShowCorrectAnswer());
    }

    private IEnumerator ShowCorrectAnswer()
    {
        // Wait for the specified delay time
        yield return new WaitForSeconds(delayTime);

        // Find the correct answer index (replace this with your logic)
        int correctAnswerIndex = 1; // Replace 1 with the actual correct index

        // Change the color of each button based on whether it's the correct answer or not
        for (int i = 0; i < answerButtons.Count; i++)
        {
            if (i == correctAnswerIndex)
            {
                // Correct answer turns green
                ChangeColor(answerButtons[i], correctColor);
            }
            else
            {
                // Wrong answers turn red
                ChangeColor(answerButtons[i], wrongColor);
            }
        }

        // Invoke the appropriate method based on the result (win/lose)
        if (correctAnswerIndex == answerButtons.IndexOf(clickedButton))
        {
            Invoke("ShowWinScreen", 2f);
        }
        else
        {
            Invoke("ShowLoseScreen", 2f);
        }

        // Disable all buttons after the correct answer is revealed (optional)
        DisableButtons();
    }

    private void ShowWinScreen()
    {
        pageEnabled = true;
        UIReferences.Instance.chooseCorrectPage.SetActive(false);
        EventManager.ChangeScreen(EventManager.CORRECT_CHOICE_PAGE);

    }

    private void ShowLoseScreen()
    {
        pageEnabled = true;
        UIReferences.Instance.chooseCorrectPage.SetActive(false);
        EventManager.ChangeScreen(EventManager.WRONG_CHOICE_PAGE);
    }

    private void ChangeColor(Button button, Color color)
    {
        // Change the button's image color
        Image buttonImage = button.GetComponent<Image>();
        if (buttonImage != null)
        {
            buttonImage.color = color;
        }
    }

    private void DisableButtons()
    {
        // Disable all buttons (optional)
        foreach (Button button in answerButtons)
        {
            button.interactable = false;
        }
    }

    private void SetupButtons()
    {
        // Set up the buttons as they were before (e.g., reset colors, enable interactability)
        foreach (Button button in answerButtons)
        {
            Image buttonImage = button.GetComponent<Image>();
            if (buttonImage != null)
            {
                buttonImage.color = originalColor; // Set to the default color or any desired initial color
            }

            button.interactable = true; // Enable interactability
        }
    }
}
