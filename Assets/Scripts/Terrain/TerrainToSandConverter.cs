using UnityEngine;
using UnityEngine.Events;

public class TerrainToSandConverter : MonoBehaviour
{
    [SerializeField] private UnityEvent<float> OnGetSandAmount;
    public void ConvertTerrainToSandAmount(float sculptAmount)
    {
        float sandAmount = SandManager.Instance.ConvertTerrainToSandAmount(sculptAmount);
        OnGetSandAmount?.Invoke(sandAmount);
    }
}
