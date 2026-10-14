
using System.Collections;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Events;

public class IntroCutsceneController : MonoBehaviour
{
    [SerializeField] private PlayableDirector introDirector;
    [SerializeField] private Transform sandAlterPoint;
    [SerializeField] private UnityEvent<Vector3> OnRaiseSand;
    [SerializeField] private UnityEvent<Vector3> OnLowerSand;
    private bool hasStarted;
    private bool hasFinished;

    public void RunCutscene()
    {
        if (!hasStarted && !hasFinished)
        {
            StartCoroutine(PlayRoutine());
        }
    }

    public void RaiseSand()
    {
        OnRaiseSand?.Invoke(sandAlterPoint.position);
    }

    public void LowerSand()
    {
        OnLowerSand?.Invoke(sandAlterPoint.position);
    }

    private IEnumerator PlayRoutine()
    {
        introDirector.Play();
        hasStarted = true;

        yield return new WaitForSeconds((float)introDirector.duration);

        hasFinished = true;
    }

    public bool HasFinished()
    {
        return hasFinished;
    }
}
