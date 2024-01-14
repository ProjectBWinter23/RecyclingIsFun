using UnityEngine;
using TMPro;
using DG.Tweening;

public class ColorChangeScript : MonoBehaviour
{
    // Define complementary colors as hex codes
    private string[] complementaryColorHexCodes = { "#FF5733", "#33FF57", "#5733FF", "#FFFF33" };

    public float colorChangeDuration = 2f;

    private TextMeshProUGUI textMesh;
    private int colorIndex = 0;

    void Start()
    {
        textMesh = GetComponent<TextMeshProUGUI>();

        // Start the color change loop
        ChangeTextColorContinuously();
    }

    void ChangeTextColorContinuously()
    {
        // Use DOTween to continuously change the color
        DOTween.Sequence()
            .Append(textMesh.DOColor(ParseHexColor(complementaryColorHexCodes[colorIndex]), colorChangeDuration))
            .AppendInterval(0.5f) // Delay between color changes
            .OnComplete(() => ChangeTextColorContinuously()); // Restart the sequence

        // Increment the color index
        colorIndex = (colorIndex + 1) % complementaryColorHexCodes.Length;
    }

    Color ParseHexColor(string hexCode)
    {
        Color color;
        ColorUtility.TryParseHtmlString(hexCode, out color);
        return color;
    }
}
