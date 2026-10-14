using System.Collections;
using UnityEngine;
using UnityEngine.Playables;

public class CutsceneManager : Singleton<CutsceneManager>
{
    [SerializeField] private IntroCutsceneController introController;
    [SerializeField] private KeyInHoleCutsceneController keyInHoleDirector;

    public void SetUpIntroCutscene()
    {
        introController.gameObject.SetActive(true);
    }

    public void DisableIntroCutscene()
    {
        introController.gameObject.SetActive(false);
    }

    public void RunIntroCutscene()
    {
        introController.RunCutscene();
    }

    public bool HasFinishedIntro()
    {
        return introController.HasFinished();
    }

    public void SetUpPlayCutscenes()
    {
        keyInHoleDirector.gameObject.SetActive(true);
    }

    public void DisablePlayCutscenes()
    {
        keyInHoleDirector.gameObject.SetActive(false);
    }
}
