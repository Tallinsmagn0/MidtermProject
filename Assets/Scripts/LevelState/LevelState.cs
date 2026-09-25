using System;
using UnityEngine;

public class LevelState : MonoBehaviour
{
    private TerrainLoader terrainLoader;

    private void Awake()
    {
        terrainLoader = GetComponent<TerrainLoader>();
    }

    public void Run()
    {
        
    }

    public void OnEnter()
    {
        terrainLoader.LoadTerrain();
    }

    public void OnExit()
    {
        terrainLoader.UnloadTerrain();
    }

    public Terrain GetLevelTerrain()
    {
        return terrainLoader.GetTerrain();
    }
}
