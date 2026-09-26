using System;
using UnityEngine;

public class PlayState : IGameState
{

    IPlayStyle playStyle;

    bool hasSetupManagers = false;
    bool hasSceneLoaded = false;

    public PlayState()
    {
        playStyle = new SoloPlayStyle();
    }

    public void OnEnter(GameManager gm)
    {
        SceneLoader.Instance.LoadSceneByName("PlayScene");
    }

    public void OnExit(GameManager gm)
    {
       // UNLOAD LEVELS AND RESET TERRAINS
    }

    public void Run(GameManager gm)
    {
        if (!hasSetupManagers)
        {
            if (!hasSceneLoaded)
            {
                hasSceneLoaded = SceneLoader.Instance.HasSceneLoaded("PlayScene");
            }
            else
            {
                SetupManagers();
            }
        }
    }

    private void SetupManagers()
    {
        SetupPlayStyleManager();
        SetupLevelManager();
        hasSetupManagers = true;
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
