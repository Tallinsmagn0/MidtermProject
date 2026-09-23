using UnityEngine;
using System;

public class TerrainController : MonoBehaviour
{
    [SerializeField] private TerrainData savedTerrainData;

    [SerializeField] private Vector3 terrainPosition;

    [SerializeField] [Range(0f, 1f)] 
    private float heightmapResolutionScalingFactor = 0.513f;

    
    public void LoadTerrain(Terrain currentTerrain)
    {
        if (currentTerrain != null)
        {
            ResetTerrainData(currentTerrain);
            ResetTerrainPosition(currentTerrain);
            ScaleTerrain(currentTerrain);
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

    private void ResetTerrainData(Terrain currentTerrain)
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

    private void ResetTerrainPosition(Terrain currentTerrain)
    {
        currentTerrain.transform.position = terrainPosition;
    }

    private void ScaleTerrain(Terrain currentTerrain)
    {
        TerrainData data = currentTerrain.terrainData;
        Vector3 dataSize = data.size;
        
        data.heightmapResolution = (int) (Mathf.Sqrt(dataSize.x * dataSize.z) * heightmapResolutionScalingFactor);
        data.size = new Vector3(dataSize.x, dataSize.y, dataSize.z);
    }
}
