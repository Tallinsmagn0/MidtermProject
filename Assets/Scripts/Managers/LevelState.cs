using System;
using UnityEngine;

public class LevelState : MonoBehaviour
{
    private TerrainController tc;

    private void Start()
    {
        tc = GetComponent<TerrainController>();
    }

    public void Run()
    {
        
    }

    public void OnEnter(LevelManager lm)
    {
        tc.LoadTerrain(lm.GetCurrentTerrain());
    }

    public void OnExit(LevelManager lm)
    {
        tc.UnloadTerrain(lm.GetCurrentTerrain());
    }
}
