using System.Collections;
using UnityEngine;
using UnityEngine.Playables;

public class CutsceneManager : Singleton<CutsceneManager>
{
    [SerializeField] private PlayableDirector introDirector;
    [SerializeField] private PlayableDirector keyInHoleDirector;

    private bool hasStartedIntro;
    private bool hasFinishedIntro;

    public void SetUpIntroCutscene()
    {
        introDirector.gameObject.SetActive(true);
    }

    public void DisableIntroCutscene()
    {
        introDirector.gameObject.SetActive(false);
    }

    public void RunIntroCutscene()
    {
        if (!hasStartedIntro && !hasFinishedIntro)
        {
            StartCoroutine(PlayIntroRoutine());
        }
    }

    private IEnumerator PlayIntroRoutine()
    {
        introDirector.Play();
        hasStartedIntro = true;

        yield return new WaitForSeconds((float)introDirector.duration);

        hasFinishedIntro = true;
    }

    public bool HasFinishedIntro()
    {
        return hasFinishedIntro;
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
