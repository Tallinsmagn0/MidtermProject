using UnityEngine;
using System;

public class TerrainController : MonoBehaviour
{
    [SerializeField] private Terrain terrain;
    [SerializeField] private TerrainData savedTerrainData;

    [SerializeField] [Range(0f, 1f)] 
    private float heightmapResolutionScalingFactor = 0.513f;

    
    public void LoadTerrain()
    {
        if (terrain != null)
        {
            ResetTerrainData();
            ScaleTerrain();
        }
        else
        {
            throw new NullReferenceException("Tried to access null current terrain while loading");
        }
    }

    public void UnloadTerrain()
    {
        if (terrain != null)
        {
            ResetTerrainData();
        }
        else
        {
            throw new NullReferenceException("Tried to access null current terrain while unloading");
        }
    }

    private void ResetTerrainData()
    {
        if (savedTerrainData != null)
        {
            TerrainData terrainData = Instantiate(savedTerrainData);

            terrain.terrainData = terrainData;
            TerrainCollider terrainCollider = terrain.gameObject.GetComponent<TerrainCollider>();
            
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

    private void ScaleTerrain()
    {
        TerrainData data = terrain.terrainData;
        Vector3 dataSize = data.size;
        
        data.heightmapResolution = (int) (Mathf.Sqrt(dataSize.x * dataSize.z) * heightmapResolutionScalingFactor);
        data.size = new Vector3(dataSize.x, dataSize.y, dataSize.z);
    }

    public Terrain GetTerrain()
    {
        return terrain;
    }
}
