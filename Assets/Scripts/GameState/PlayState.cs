using System;
using UnityEngine;

public class PlayState : IGameState
{

    IPlayStyle playStyle;

    bool hasSetupScene = false;
    bool hasSceneLoaded = false;

    public PlayState() { }

    public void OnEnter(GameManager gm)
    {
        if (!SceneLoader.Instance.HasSceneLoaded("PlayScene"))
        {
            SceneLoader.Instance.LoadSceneByName("PlayScene");
        }
    }

    public void OnExit(GameManager gm) { }

    public void Run(GameManager gm)
    {
        SetupManagersOnSceneLoad();
    }

    private void SetupManagersOnSceneLoad()
    {
        if (!hasSetupScene)
        {
            if (!hasSceneLoaded)
            {
                hasSceneLoaded = SceneLoader.Instance.HasSceneLoaded("PlayScene");
            }
            else
            {
                SetupScene();
            }
        }
    }

    private void SetupScene()
    {
        SetupPlayStyleManager();
        SetupLevelManager();
        hasSetupScene = true;
    }

    private void SetupPlayStyleManager()
    {
        if (PlayStyleManager.Instance != null)
        {
            PlayStyleManager.Instance.gameObject.SetActive(true);
            playStyle = PlayStyleManager.Instance.PlayStyle;
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
