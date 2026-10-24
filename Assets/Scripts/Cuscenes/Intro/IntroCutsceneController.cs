using UnityEngine;
using UnityEngine.Events;

public class IntroCutsceneController : CutsceneController
{
    [SerializeField] private Transform sandAlterPoint;
    [SerializeField] private UnityEvent<Vector3> OnRaiseSand;
    [SerializeField] private UnityEvent<Vector3> OnLowerSand;

    public void RaiseSand()
    {
        OnRaiseSand?.Invoke(sandAlterPoint.position);
    }

    public void LowerSand()
    {
        OnLowerSand?.Invoke(sandAlterPoint.position);
    }

    public void UnsetPlayOnAwake()
    {
        director.playOnAwake = false;
    }
}
