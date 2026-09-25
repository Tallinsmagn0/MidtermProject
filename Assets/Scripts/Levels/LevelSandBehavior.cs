using UnityEngine;
using UnityEngine.Events;

public class LevelSandBehavior : MonoBehaviour
{
    [SerializeField] private UnityEvent<float> OnUpdateSandAmount;
    [SerializeField] private UnityEvent OnResetMaxSandAmount;
    [SerializeField] private float amountMultiplier;
    private float maxSandAmount;
    private float sandAmount;

    public void UpdateSandAmount(float deltaAmount)
    {
        Debug.Log("deltaAmount: " + deltaAmount);
        float newAmount = Mathf.Clamp(sandAmount - deltaAmount * amountMultiplier, 0, maxSandAmount);

        if (newAmount != sandAmount)
        {
            sandAmount = newAmount;
            Debug.Log("SandAmount: " + sandAmount);
            OnUpdateSandAmount?.Invoke(sandAmount / maxSandAmount);
        }
    }

    public void ResetMaxSandAmount()
    {
        maxSandAmount = LevelManager.Instance.GetMaxSandAmount();
        sandAmount = maxSandAmount;
        OnResetMaxSandAmount?.Invoke();
    }
}
