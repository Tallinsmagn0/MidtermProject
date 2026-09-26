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
        if (!SceneLoader.Instance.HasSceneLoaded("PlayScene"))
        {
            SceneLoader.Instance.LoadSceneByName("PlayScene");
        }
    }

    public void OnExit(GameManager gm)
    {
       // UNLOAD LEVELS AND RESET TERRAINS
    }

    public void Run(GameManager gm)
    {
        SetupManagersOnSceneLoad();
    }

    private void SetupManagersOnSceneLoad()
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
            PlayStyleManager.Instance.gameObject.SetActive(true);
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
            LevelManager.Instance.gameObject.SetActive(true);
            LevelManager.Instance.LoadFirstLevel();
        }
        else
        {
            throw new NullReferenceException("No level manager detected");
        }
    }
}
