using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LevelManager : Singleton<LevelManager>
{
    [SerializeField] private LevelController[] levels;
    [SerializeField] private UnityEvent OnLevelChange;

    private int currLevel;

    public void LoadFirstLevel()
    {
        currLevel = 1;
        levels[currLevel]?.Load();
        OnLevelChange?.Invoke();
    }

    private void OnDisable() 
    {
        UnloadLevels();
    }

    public void UnloadLevels()
    {
        foreach (LevelController level in levels)
        {
            level?.Unload();
        }
    }

    public void IncrementLevel()
    {
        levels[currLevel]?.Unload();
        currLevel++;

        if (currLevel < levels.Length)
        {
            levels[currLevel]?.Load();
            OnLevelChange?.Invoke();

            if (currLevel == levels.Length - 1)
            {
                GameManager.Instance.WinGame();
            }
        }
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

    public Transform GetCurrentDoorCameraPivot()
    {
        return levels[currLevel]?.GetDoorCameraPivot();
    }

    public Animator GetCurrentDoorAnimator()
    {
        return levels[currLevel]?.GetDoorAniamtor();
    }

    public List<Collider> GetTerrainExclusionColliders()
    {
        return levels[currLevel]?.GetTerrainExclusionColliders();
    }

    public void ClearedCurrentKeyAndHole()
    {
        levels[currLevel]?.ClearedKeyAndHole();
    }

    public void DeactivateCurrentKeyAndHole()
    {
        levels[currLevel]?.DeactivateKeyAndHole();
    }
}
