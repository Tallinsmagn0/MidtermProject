using System.Collections;
using UnityEngine;
using UnityEngine.Playables;

public class CutsceneManager : Singleton<CutsceneManager>
{
    [SerializeField] private IntroCutsceneController introController;
    [SerializeField] private KeyInHoleCutsceneController keyInHoleDirector;
    [SerializeField] private GameOverCutsceneController gameOverCutsceneController;

    public void ActivateIntroCutscene()
    {
        introController.gameObject.SetActive(true);
    }

    public void DeactivateIntroCutscene()
    {
        introController.gameObject.SetActive(false);
    }

    public void ActivatePlayCutscenes()
    {
        keyInHoleDirector.gameObject.SetActive(true);
    }

    public void RunIntroCutscene()
    {
        introController.PlayCutscene();
    }

    public bool HasFinishedIntro()
    {
        return introController.HasFinished();
    }

    public void SetUpKeyInHoleCutscene(KeyInHoleCutsceneTrigger trigger)
    {
        keyInHoleDirector.SetUpFromTrigger(trigger);
    }

    public void RunKeyInHoleCutscene()
    {
        keyInHoleDirector.PlayCutscene();
    }

    public bool HasFinishedCurrentKeyInHoleCutscene()
    {
        return keyInHoleDirector.HasFinished();
    }

    public void ResetKeyInHoleVariables()
    {
        keyInHoleDirector.ResetCutsceneVariables();
    }

    public Transform GetKeyInHoleBallMovePoint()
    {
        return keyInHoleDirector.GetKeyBallMovePoint();
    }

    public float GetKeyInHoleBallMoveDuration()
    {
        return keyInHoleDirector.GetKeyBallMoveDuration();
    }

    public void ActivateGameOverCutscene()
    {
        gameOverCutsceneController.gameObject.SetActive(true);
    }

    public void RunGameOverCutscene()
    {
        gameOverCutsceneController.PlayCutscene();
    }

    public bool HasFinishedGameOverCutscene()
    {
        return gameOverCutsceneController.HasFinished();
    }
}
