using System;
using UnityEngine;

public class IntroState : IGameState
{
    bool hasSetupScene = false;
    bool hasSceneLoaded = false;

    public  IntroState() { }

    public void OnEnter(GameManager gm)
    {
        if (!SceneLoader.Instance.HasSceneLoaded("PlayScene"))
        {
            SceneLoader.Instance.LoadSceneByName("PlayScene");
        }
    }

    public void OnExit(GameManager gm)
    {
        CutsceneManager.Instance.DisableIntroCutscene();
    }

    public void Run(GameManager gm)
    {
        if (hasSetupScene)
        {
            RunIntroCutscene(gm);
        }
        else
        {
            SetupManagersOnSceneLoad();
        }
    }

    private void SetupManagersOnSceneLoad()
    {
        if (!hasSceneLoaded)
        {
            hasSceneLoaded = SceneLoader.Instance.HasSceneLoaded("PlayScene");
        }
        else
        {
            SetUpScene();
        }
    }

    private void SetUpScene()
    {
        SetUpManagers();
        hasSetupScene = true;
    }

    private void SetUpManagers()
    {
        SetUpPlayStyleManager();
        SetUpLevelManager();
        SetUpCutsceneManager();
        SetUpSandManager();
        SetUpUIManager();
    }

    private void SetUpPlayStyleManager()
    {
        if (PlayStyleManager.Instance != null)
        {
            PlayStyleManager.Instance.enabled = true;
            PlayStyleManager.Instance.PlayStyle = new SoloPlayStyle();
        }
        else
        {
            throw new NullReferenceException("No play style manager detected");
        }
    }

    private void SetUpLevelManager()
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

    private void SetUpCutsceneManager()
    {
        if (CutsceneManager.Instance != null)
        {
            CutsceneManager.Instance.enabled = true;
            CutsceneManager.Instance.SetUpIntroCutscene();
        }
        else
        {
            throw new NullReferenceException("No cutscene manager detected");
        }
    }

    private void SetUpSandManager()
    {
        if (SandManager.Instance != null)
        {
            SandManager.Instance.enabled = true;
        }
        else
        {
            throw new NullReferenceException("No sand manager detected");
        }
    }

    private void SetUpUIManager()
    {
        if (UIManager.Instance != null)
        {
            UIManager.Instance.enabled = true;
        }
        else
        {
            throw new NullReferenceException("No UI manager detected");
        }
    }

    private void RunIntroCutscene(GameManager gm)
    {
        CutsceneManager.Instance.RunIntroCutscene();

        if (CutsceneManager.Instance.HasFinishedIntro())
        {
            IGameState playState = new PlayState();
            gm.Transition(playState);
        }
    }
}
