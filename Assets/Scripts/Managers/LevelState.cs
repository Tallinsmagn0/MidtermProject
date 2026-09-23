using System;
using UnityEngine;

public class LevelState : MonoBehaviour
{
    private TerrainController tc;

    private void Awake()
    {
        tc = GetComponent<TerrainController>();
    }

    public void Run()
    {
        
    }

    public void OnEnter()
    {
        tc.LoadTerrain();
    }

    public void OnExit()
    {
        tc.UnloadTerrain();
    }

    public Terrain GetLevelTerrain()
    {
        return tc.GetTerrain();
    }
}
