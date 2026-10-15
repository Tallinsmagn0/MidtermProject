using UnityEngine;

public class SandManager : Singleton<SandManager>
{
    [SerializeField] private float terrainToSandAmount;

    public float ConvertTerrainToSandAmount(float sculptAmount)
    {
        float maxSandAmount = LevelManager.Instance.GetMaxSandAmount();
        int terrainResolution = LevelManager.Instance.GetCurrentTerrainResolution();
        float sandAmount = maxSandAmount * (1f - terrainToSandAmount * sculptAmount / (terrainResolution * terrainResolution));
        return sandAmount;
    }

    public float GetMaxTerrainSculptAmount()
    {
        int terrainResolution = LevelManager.Instance.GetCurrentTerrainResolution();
        return terrainResolution * terrainResolution / terrainToSandAmount;
    }
}
