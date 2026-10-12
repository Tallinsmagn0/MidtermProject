using System;
using UnityEngine;

public class MenuState : IGameState
{
    public void OnEnter(GameManager gm)
    {
        if (MenuManager.Instance != null)
        {
            MenuManager.Instance.enabled = true;
        } 
        else
        {
            throw new NullReferenceException("No menu manager detected");
        }
    }

    public void OnExit(GameManager gm)
    {
        if (MenuManager.Instance != null)
        {
            MenuManager.Instance.gameObject.SetActive(false);
        }
        else
        {
            throw new NullReferenceException("No menu manager detected");
        }
    }

    public void Run(GameManager gm)
    {
        if (MenuManager.Instance.HasPressedPlay())
        {
            IGameState introState = new IntroState();
            gm.Transition(introState);
        }
    }
}
