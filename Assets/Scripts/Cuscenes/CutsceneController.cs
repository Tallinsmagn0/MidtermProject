
using System.Collections;
using UnityEngine;
using UnityEngine.Playables;

public abstract class CutsceneController : MonoBehaviour
{
    [SerializeField] protected PlayableDirector director;
    protected bool hasStarted;
    protected bool hasFinished;

    public void PlayCutscene()
    {
        if (!hasStarted && !hasFinished)
        {
            StartCoroutine(PlayRoutine());
        }
    }

    private IEnumerator PlayRoutine()
    {
        director.Play();
        hasStarted = true;

        yield return new WaitForSeconds((float)director.duration);

        hasFinished = true;
    }

    public bool HasFinished()
    {
        return hasFinished;
    }

    public void ResetCutsceneVariables()
    {
        hasStarted = false;
        hasFinished = false;
    }
}
