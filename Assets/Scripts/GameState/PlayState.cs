using System;
using UnityEngine;

public class PlayState : IGameState
{
    public PlayState() { }

    public void OnEnter(GameManager gm)
    {
        SetupPlayStyle();
        CutsceneManager.Instance.ActivatePlayCutscenes();
        UIManager.Instance.SetPlayerStatsVisibility(true);
    }

    public void OnExit(GameManager gm) { }

    public void Run(GameManager gm)
    {
        if (CutsceneManager.Instance.HasFinishedCurrentKeyInHoleCutscene())
        {
            CutsceneManager.Instance.ResetKeyInHoleVariables();
            LevelManager.Instance.DeactivateCurrentKeyAndHole();
            PlayStyleManager.Instance.EnablePlayerControls();
        }
    }

    private void SetupPlayStyle()
    {
        if (PlayStyleManager.Instance != null)
        {
            PlayStyleManager.Instance.SetPlayStyleFromScene();
        }
        else
        {
            throw new NullReferenceException("No play style manager detected");
        }
    }
}
