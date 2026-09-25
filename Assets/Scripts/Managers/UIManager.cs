using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : Singleton<UIManager>
{
    [SerializeField] private TMP_Text brushSizeText;
    [SerializeField] private Slider sandSlider;

    public void SetBrushSizeText(int brushSize)
    {
        brushSizeText.text = $"{brushSize}";
    }

    public void SetSliderMaxValue(float maxValue)
    {
        sandSlider.maxValue = maxValue;
        SetSliderValue(sandSlider.maxValue);
    }

    public void SetSliderValue(float value)
    {
        sandSlider.value = value;
    }
    
}
