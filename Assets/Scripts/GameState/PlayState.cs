using System;
using UnityEngine;

public class PlayState : IGameState
{
    public PlayState() { }

    public void OnEnter(GameManager gm)
    {
        SetupPlayStyle();
        CutsceneManager.Instance.SetUpPlayCutscenes();
    }

    public void OnExit(GameManager gm) { }

    public void Run(GameManager gm)
    {
        
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
