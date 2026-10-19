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

    public void OnExit(GameManager gm)
    {
        UIManager.Instance.SetPlayVisibility(false);
    }

    public void Run(GameManager gm)
    {
        if (CutsceneManager.Instance.HasFinishedCurrentKeyInHoleCutscene())
        {
            CutsceneManager.Instance.ResetKeyInHoleVariables();
            LevelManager.Instance.DeactivateCurrentKeyAndHole();
            PlayerManager.Instance.EnablePlayerControls();
        }
    }

    private void SetupPlayStyle()
    {
        if (PlayerManager.Instance != null)
        {
            PlayerManager.Instance.SetPlayStyleFromScene();
        }
        else
        {
            throw new NullReferenceException("No play style manager detected");
        }
    }
}
