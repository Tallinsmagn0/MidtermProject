using UnityEngine;
using UnityEngine.Events;

public class SandAmountBehavior : MonoBehaviour
{

    [SerializeField] private UnityEvent<float> OnUpdateSandAmount;
    [SerializeField] private UnityEvent<float> OnResetMaxSandAmount;
    private float sandAmount;

    public void UpdateSandAmount(float _sandAmount)
    {
        float maxSandAmount = LevelManager.Instance.GetMaxSandAmount();
        float clampedSandAmount = Mathf.Clamp(_sandAmount , 0, maxSandAmount);

        if (clampedSandAmount != sandAmount)
        {
            sandAmount = clampedSandAmount;
            OnUpdateSandAmount?.Invoke(sandAmount);
        }
    }

    public void ResetMaxSandAmount()
    {
        sandAmount = LevelManager.Instance.GetMaxSandAmount();
        OnResetMaxSandAmount?.Invoke(sandAmount);
    }
}
