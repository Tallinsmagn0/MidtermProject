using System;
using UnityEngine;

public class PlayState : IGameState
{

    IPlayStyle playStyle;

    public PlayState()
    {
        playStyle = new SoloPlayStyle();
    }

    public void OnEnter(GameManager gm)
    {
        // LOAD SCENE

        SetupManagers();
    }

    public void OnExit(GameManager gm)
    {
       // UNLOAD LEVELS AND RESET TERRAINS
    }

    public void Run(GameManager gm)
    {
        
    }

    private void SetupManagers()
    {
        SetupPlayStyleManager();
        SetupLevelManager();
    }

    private void SetupPlayStyleManager()
    {
        if (PlayStyleManager.Instance != null)
        {
            PlayStyleManager.Instance.enabled = true;
            PlayStyleManager.Instance.PlayStyle = playStyle;
        }
        else
        {
            throw new NullReferenceException("No play style manager detected");
        }
    }

    private void SetupLevelManager()
    {
        if (LevelManager.Instance != null)
        {
            LevelManager.Instance.enabled = true;
            LevelManager.Instance.LoadFirstLevel();
        }
        else
        {
            throw new NullReferenceException("No level manager detected");
        }
    }
}
