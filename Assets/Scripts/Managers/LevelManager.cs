using System;
using UnityEngine;
using UnityEngine.Events;

public class LevelManager : Singleton<LevelManager>
{
    [SerializeField] private LevelController[] levels;
    [SerializeField] private UnityEvent OnLevelChange;

    private int currLevel;

    public void LoadFirstLevel()
    {
        currLevel = 0;
        levels[currLevel]?.Load();
        OnLevelChange?.Invoke();
    }

    public void IncrementLevel()
    {
        levels[currLevel]?.Unload();
        currLevel++;
        levels[currLevel]?.Load();
        OnLevelChange?.Invoke();
    }

    public Terrain GetCurrentTerrain()
    {
        return levels[currLevel]?.GetLevelTerrain();
    }

    public int GetCurrentTerrainResolution()
    {
        Terrain terrain = GetCurrentTerrain();

        if (terrain != null)
        {
            TerrainData terrainData = terrain.terrainData;

            if (terrainData != null)
            {
                return terrainData.heightmapResolution;
            }
            else
            {
                throw new NullReferenceException("Can't return resolution because terrainData is null");
            }
        }
        else
        {
            throw new NullReferenceException("Can't return resolution because terrain is null");
        }
    }

    public float GetMaxSandAmount()
    {
        return levels[currLevel].GetMaxSandAmount();
    }
}
