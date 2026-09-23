using System;
using UnityEngine;

public class LevelState : MonoBehaviour
{
    [SerializeField]
    private TerrainData savedTerrainData;

    [SerializeField]
    private Vector3 terrainPosition;

    [SerializeField] [Range(0f, 1f)]
    private float heightmapResolutionScalingFactor;

    public void Run()
    {
        
    }

    public void OnEnter(LevelManager lm)
    {
        LoadTerrain(lm.GetCurrentTerrain());
    }

    public void OnExit(LevelManager lm)
    {
        UnloadTerrain(lm.GetCurrentTerrain());
    }

    public void LoadTerrain(Terrain currentTerrain)
    {
        if (currentTerrain != null)
        {
            ResetTerrainData(currentTerrain);
            ResetTerrainPosition(currentTerrain);
            ScaleTerrainHeightmapResolution(currentTerrain);
        }
        else
        {
            throw new NullReferenceException("Tried to access null current terrain while loading");
        }
    }

    public void UnloadTerrain(Terrain currentTerrain)
    {
        if (currentTerrain != null)
        {
            ResetTerrainData(currentTerrain);
        }
        else
        {
            throw new NullReferenceException("Tried to access null current terrain while unloading");
        }
    }

    public void ResetTerrainData(Terrain currentTerrain)
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
            else
            {
                throw new NullReferenceException("Current terrain collider was null");
            }
        
        }
        else
        {
            throw new NullReferenceException("Saved terrain for current level was null");
        }
    }

    public void ResetTerrainPosition(Terrain currentTerrain)
    {
        currentTerrain.transform.position = terrainPosition;
    }

    public void ScaleTerrainHeightmapResolution(Terrain currentTerrain)
    {
        TerrainData data = currentTerrain.terrainData;
        Vector3 dataSize = data.size;
        
        data.heightmapResolution = (int) (Mathf.Sqrt(dataSize.x * dataSize.z) * heightmapResolutionScalingFactor);
        data.size = new Vector3(dataSize.x, dataSize.y, dataSize.z);
    }
}
