using UnityEngine;

public class SandManager : Singleton<SandManager>
{
    [SerializeField] private float terrainToSandAmount;

    public float ConvertTerrainToSandAmount(float sculptAmount)
    {
        float maxSandAmount = LevelManager.Instance.GetMaxSandAmount();
        float sandAmount = maxSandAmount * (1f -  sculptAmount / GetMaxTerrainSculptAmount());
        return sandAmount;
    }

    public float GetMaxTerrainSculptAmount()
    {
        int terrainResolution = LevelManager.Instance.GetCurrentTerrainResolution();
        float maxSandAmount = LevelManager.Instance.GetMaxSandAmount();
        return Mathf.Clamp(maxSandAmount * terrainResolution * terrainResolution / terrainToSandAmount, 0f, terrainResolution * terrainResolution);
    }
}
