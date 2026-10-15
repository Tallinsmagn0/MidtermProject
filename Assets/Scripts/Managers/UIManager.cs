using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : Singleton<UIManager>
{
    [SerializeField] private GameObject playerStatsObject;
    [SerializeField] private TMP_Text brushSizeText;
    [SerializeField] private Slider sandSlider;

    public void SetBrushSizeText(int brushSize)
    {
        brushSizeText.text = $"{brushSize}";
    }

    public void SetSliderMaxValue(float maxValue)
    {
        if (sandSlider != null)
        {
            sandSlider.maxValue = maxValue;
        }
        SetSliderValue(sandSlider.maxValue);
    }

    public void SetSliderValue(float value)
    {
        if (sandSlider != null)
        {
            sandSlider.value = value;
        }
    }

    public void SetPlayerStatsVisibility(bool isActive)
    {
        playerStatsObject.SetActive(isActive);
    }
    
}
