using System;
using UnityEngine;

public class LevelState : MonoBehaviour
{
    private TerrainController terrainController;

    private void Awake()
    {
        terrainController = GetComponent<TerrainController>();
    }

    public void Run()
    {
        
    }

    public void OnEnter()
    {
        terrainController.LoadTerrain();
    }

    public void OnExit()
    {
        terrainController.UnloadTerrain();
    }

    public Terrain GetLevelTerrain()
    {
        return terrainController.GetTerrain();
    }
}
