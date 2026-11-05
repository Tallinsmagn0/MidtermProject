using UnityEngine;
using UnityEngine.Events;

public class TerrainToSandConverter : MonoBehaviour
{
    [SerializeField] private UnityEvent<float> OnGetSandAmount;
    public void ConvertTerrainToSandAmount(float sculptAmount)
    {
        float sandAmount = SandManager.Instance.ConvertTerrainToSandAmount(sculptAmount);
        Debug.Log("Sand amount: " + sandAmount);
        OnGetSandAmount?.Invoke(sandAmount);
    }
}
