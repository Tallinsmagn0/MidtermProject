using System;
using UnityEngine;

public class LevelData : MonoBehaviour
{
    [SerializeField] private float maxSandAmount;
    private TerrainLoader terrainLoader;

    private void Awake()
    {
        terrainLoader = GetComponent<TerrainLoader>();
    }

    public void Load()
    {
        terrainLoader.LoadTerrain();
    }

    public void Unload()
    {
        terrainLoader.UnloadTerrain();
    }

    public Terrain GetLevelTerrain()
    {
        return terrainLoader.GetTerrain();
    }

    public float GetMaxSandAmount()
    {
        return maxSandAmount;
    }
}
