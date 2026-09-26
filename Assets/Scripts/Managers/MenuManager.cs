using UnityEngine;
using UnityEngine.UI;

public class MenuManager : Singleton<MenuManager>
{
    bool pressedPlay = false;

    public void ClickPlayButton()
    {
        pressedPlay = true;
    }

    public bool HasPressedPlay()
    {
        return pressedPlay;
    }
}
