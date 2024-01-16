using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TextToggle : MonoBehaviour
{
    private Button hintButton;

    private void Start()
    {
        hintButton = GetComponent<Button>();
        hintButton.onClick.AddListener(ToggleHint);
    }

    private void ToggleHint()
    {
        TMP_Text text = hintButton.GetComponentInChildren<TMP_Text>();
        text.enabled = !text.enabled;
    }
}
