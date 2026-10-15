using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : Singleton<UIManager>
{
    [SerializeField] private GameObject playerStatsObject;
    [SerializeField] private TMP_Text brushSizeText;
    [SerializeField] private Slider sandSlider;
    [SerializeField] private FadeText introRaiseText;
    [SerializeField] private FadeText introLowerText;
    [SerializeField] private TMP_Text timerText;

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

    public void FadeInIntroRaiseText()
    {
        if (introRaiseText != null)
        {
            introRaiseText.StartFadeIn();
        }
    }

    public void FadeInIntroLowerText()
    {
        if (introLowerText != null)
        {
            introLowerText.StartFadeIn();
        }
    }

    public void FadeOutIntroTexts()
    {
        introRaiseText.StartFadeOut();
        introLowerText.StartFadeOut();
    }

    public void SetIsTimerTextActive(bool isActive)
    {
        timerText.gameObject.SetActive(isActive);
    }

    public void SetTimerValue(float currTime)
    {
        int minutes = Mathf.FloorToInt(currTime / 60f);
        int seconds = Mathf.FloorToInt(currTime % 60f);

        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
