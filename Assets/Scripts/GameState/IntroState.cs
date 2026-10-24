using System;
using UnityEngine;

public class IntroState : LoadableState
{
    public IntroState() : base()
    {
        sceneName = "PlayScene";
    }

    public IntroState (bool loadOnEnter) : base(loadOnEnter)
    {
        sceneName = "PlayScene";
    }

    public override void OnExit(GameManager gm)
    {
        CutsceneManager.Instance.DeactivateIntroCutscene();
    }

    protected override void RunScene(GameManager gm)
    {
        // RunIntroCutscene(gm);
        gm.Transition(new PlayState());
    }

    protected override void SetUpScene()
    {
        SetUpPlayerManager();
        SetUpLevelManager();
        SetUpCutsceneManager();
        SetUpSandManager();
        SetUpUIManager();
    }

    private void SetUpPlayerManager()
    {
        if (PlayerManager.Instance != null)
        {
            PlayerManager.Instance.enabled = true;
            PlayerManager.Instance.PlayStyle = new SoloPlayStyle();
        }
        else
        {
            throw new NullReferenceException("No player manager detected");
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
            CutsceneManager.Instance.ActivateIntroCutscene();
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
            UIManager.Instance.SetPlayerStatsVisibility(false);
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
            gm.StartGameplay();
        }
    }
}
