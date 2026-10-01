using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    [SerializeField] [Range(0f, 1f)] private float maxSandAmount;
    [SerializeField] private GameObject doorObject;
    [SerializeField] private string doorCameraTag;
    [SerializeField] private ILoader[] loaders;
    [SerializeField] private TerrainExclusionController terrainExclusionController;

    private TerrainLoader terrainLoader;


    void Awake()
    {
        loaders = GetComponents<ILoader>();

        if (loaders.Length > 0)
        {
            terrainLoader = loaders.OfType<TerrainLoader>().FirstOrDefault();
        }
    }

    public void Load()
    {
        foreach (ILoader loader in loaders)
        {
            loader.Load();
        }
    }

    public void Unload()
    {
        foreach (ILoader loader in loaders)
        {
            loader.Unload();
        }
        terrainExclusionController.DisableExclusionColliders();
    }

    public Terrain GetLevelTerrain()
    {
        if (terrainLoader != null)
        {
            return terrainLoader.GetTerrain();
        }
        else
        {
            throw new NullReferenceException("No TerrainLoader found");
        }
    }

    public float GetMaxSandAmount()
    {
        return maxSandAmount;
    }

    public Transform GetDoorCameraPivot()
    {
        Transform[] allChildren = doorObject.GetComponentsInChildren<Transform>(true);
    
        foreach (Transform child in allChildren)
        {
            if (child != transform && child.CompareTag(doorCameraTag))
            {
                return child.gameObject.transform;
            }
        }
        return null;
    }

    public Animator GetDoorAniamtor()
    {
        return doorObject.GetComponent<Animator>();
    }

    public List<Collider> GetTerrainExclusionColliders()
    {
        return terrainExclusionController.GetExclusionColliders();
    }
}
