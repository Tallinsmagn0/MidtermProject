using System;
using Unity.VisualScripting;
using UnityEngine;

public class LevelState : MonoBehaviour
{
    [SerializeField]
    TerrainData savedTerrainData;

    [SerializeField]
    Terrain currentTerrain;

    public void Run()
    {
        
    }

    public void OnEnter()
    {
        LoadTerrain();
    }

    public void OnExit()
    {
        LoadTerrain();
    }

    public void LoadTerrain()
    {
        if (savedTerrainData != null)
        {
            TerrainData terrainData = Instantiate(savedTerrainData);
            currentTerrain.terrainData = terrainData;

            TerrainCollider terrainCollider = currentTerrain.gameObject.GetComponent<TerrainCollider>();

            if (terrainCollider != null)
            {
                terrainCollider.terrainData = savedTerrainData;
            }
        }
        else
        {
            throw new NullReferenceException("Saved terrain for current level was null");
        }
    }

    public Terrain GetCurrentTerrain()
    {
        return currentTerrain;
    }
}
