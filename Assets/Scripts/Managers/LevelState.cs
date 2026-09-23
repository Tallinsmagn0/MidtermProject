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

    public void OnEnter(LevelManager lm)
    {
        if (lm != null)
        {
            tc.LoadTerrain(lm.GetCurrentTerrain());
        }
    }

    public void OnExit(LevelManager lm)
    {
        if (lm != null)
        {
            tc.UnloadTerrain(lm.GetCurrentTerrain());
        }
    }
}
