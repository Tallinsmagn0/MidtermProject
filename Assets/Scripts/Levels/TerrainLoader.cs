using UnityEngine;
using System;

public class TerrainLoader : MonoBehaviour, ILoader
{
    [SerializeField] private Terrain terrain;
    [SerializeField] private TerrainData savedTerrainData;

    [SerializeField] [Range(0f, 1f)] 
    private float heightmapResolutionScalingFactor = 0.513f;
    [SerializeField] Transform bottomRightCorner;
    [SerializeField] Transform topLeftCorner;

    
    public void Load()
    {
        if (terrain != null)
        {
            ResetTerrainData();
            ScaleAndPositionTerrain();
            ResetExclusionColliders();
        }
        else
        {
            throw new NullReferenceException("Tried to access null current terrain while loading");
        }
    }

    public void Unload()
    {
        if (terrain != null)
        {
            ResetTerrainData();
            DisableTerrainCollider();
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

            ResetTerrainColliderData();
        }
        else
        {
            throw new NullReferenceException("Saved terrain for current level was null");
        }
    }

    private void ResetTerrainColliderData()
    {
        TerrainCollider terrainCollider = terrain.gameObject.GetComponent<TerrainCollider>();
            
            if (terrainCollider != null)
            {
                terrainCollider.terrainData = terrain.terrainData;
            } 
            else
            {
                throw new NullReferenceException("Current terrain collider was null");
            }
    }

    private void ScaleAndPositionTerrain()
    {
        terrain.transform.position = bottomRightCorner.position;
        Vector3 terrainSize = topLeftCorner.position - terrain.transform.position;

        TerrainData terrainData = terrain.terrainData;
        terrainData.size = new Vector3(terrainSize.x, terrainData.size.y, terrainSize.z);

        ResizeTerrainToFitResolution();
    }

    private void ResizeTerrainToFitResolution()
    {
        TerrainData data = terrain.terrainData;
        Vector3 dataSize = data.size;
        
        data.heightmapResolution = (int) (Mathf.Sqrt(dataSize.x * dataSize.z) * heightmapResolutionScalingFactor);
        data.size = new Vector3(dataSize.x, dataSize.y, dataSize.z);
    }

    private void ResetExclusionColliders()
    {
        ExclusionColliderController exclusionColliderController = terrain.GetComponentInChildren<ExclusionColliderController>();

        if (exclusionColliderController != null)
        {
            exclusionColliderController.SetBorderColliders();
        }
        else
        {
            throw new NullReferenceException("ExclusionColliderController not found on current terrain");
        }
    }

    private void DisableTerrainCollider()
    {
        TerrainCollider terrainCollider = terrain.gameObject.GetComponent<TerrainCollider>();

        if (terrainCollider != null)
        {
            terrainCollider.enabled = false;
        }
        else
        {
            throw new NullReferenceException("Current terrain collider was null");
        }
    }

    public Terrain GetTerrain()
    {
        return terrain;
    }
}
