using System;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : Singleton<MenuManager>
{
    [SerializeField] string startButtonTag;
    bool pressedPlay = false;

    public void ClickPlayButton()
    {
        pressedPlay = true;
    }

    public bool HasPressedPlay()
    {
        return pressedPlay;
    }

    public void ReconnectListeners()
    {
        GameObject startButtonObject = GameObject.FindWithTag(startButtonTag);

        if (startButtonObject != null)
        {
            Button startButton = startButtonObject.GetComponent<Button>();

            if (startButton != null)
            {
                startButton.onClick.AddListener(ClickPlayButton);
            }
            else
            {
                throw new NullReferenceException("No button component found");
            }
        }
        else
        {
            throw new NullReferenceException("No object found with start button tag");
        }
    }
}
