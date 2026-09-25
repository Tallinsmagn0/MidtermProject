using UnityEngine;

public class SandManager : Singleton<SandManager>
{
    [SerializeField] private float terrainToSandAmount;

    public float ConvertTerrainToSandAmount(float sculptAmount)
    {
        float maxSandAmount = LevelManager.Instance.GetMaxSandAmount();
        int terrainResolution = LevelManager.Instance.GetCurrentTerrainResolution();
        return maxSandAmount * (1f - terrainToSandAmount * sculptAmount / (terrainResolution * terrainResolution));
    }

    public bool CanSculptMoreTerrain(float sculptAmount)
    {
        float sandAmount = ConvertTerrainToSandAmount(sculptAmount);
        return sandAmount > 0f && sandAmount < LevelManager.Instance.GetMaxSandAmount();
    }
}
