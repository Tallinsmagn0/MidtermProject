using System;
using UnityEngine;

public class MenuState : LoadableState
{

    public MenuState() : base()
    {
        sceneName = "MenuScene";
    }

    public override void OnExit(GameManager gm)
    {
        if (MenuManager.Instance != null)
        {
            UnityEngine.Object.Destroy(MenuManager.Instance.gameObject);
        }
        else
        {
            throw new NullReferenceException("No menu manager detected");
        }
    }

    protected override void RunScene(GameManager gm)
    {
       if (MenuManager.Instance.HasPressedPlay())
        {
            gm.StartGame();
        } 
    }

    protected override void SetUpScene()
    {
        SetUpManagers();
    }

    private void SetUpManagers()
    {
        if (MenuManager.Instance != null)
        {
            MenuManager.Instance.enabled = true;
            MenuManager.Instance.ReconnectListeners();
        } 
        else
        {
            throw new NullReferenceException("No menu manager detected");
        }
    }
}
