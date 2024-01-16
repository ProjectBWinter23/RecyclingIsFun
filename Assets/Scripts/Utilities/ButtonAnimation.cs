using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class ButtonAnimation : MonoBehaviour
{
    [SerializeField] private float scaleFactor = 1.2f; // Adjust the scale factor for the animation
    [SerializeField] private float animationDuration = 0.5f; // Adjust the duration of the animation
    [SerializeField] private Color highlightColor = Color.green; // Adjust the highlight color

    private RectTransform buttonRectTransform;
    private Image buttonImage;
    private Color originalColor;

    private void Start()
    {
        // Get the RectTransform and Image components of the button
        buttonRectTransform = GetComponent<RectTransform>();
        buttonImage = GetComponent<Image>();

        // Store the original color
        originalColor = buttonImage.color;

        // Start the button animation
        StartButtonAnimation();
    }

    private void StartButtonAnimation()
    {
        // Set the initial scale
        buttonRectTransform.localScale = Vector3.one;

        // Create a sequence for the animation
        Sequence buttonSequence = DOTween.Sequence();

        // Add the increase size animation
        buttonSequence.Append(buttonRectTransform.DOScale(scaleFactor, animationDuration));

        // Add the color change animation
        buttonSequence.Join(buttonImage.DOColor(highlightColor, animationDuration));

        // Add the decrease size animation (reverse of the increase)
        buttonSequence.Append(buttonRectTransform.DOScale(1f, animationDuration));

        // Add the color revert animation
        buttonSequence.Join(buttonImage.DOColor(originalColor, animationDuration));

        // Set the loop type to Yoyo to make it go back and forth
        buttonSequence.SetLoops(-1, LoopType.Yoyo);
    }
}