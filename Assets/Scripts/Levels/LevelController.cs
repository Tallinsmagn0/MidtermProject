using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    [SerializeField] private float maxSandAmount;
    [SerializeField] private GameObject doorObject;
    [SerializeField] private string doorCameraTag;
    [SerializeField] private ILoader[] loaders;
    [SerializeField] private TerrainExclusionController terrainExclusionController;

    private TerrainLoader terrainLoader;
    private KeyAndHoleLoader keyAndHoleLoader;


    void Awake()
    {
        loaders = GetComponents<ILoader>();

        if (loaders.Length > 0)
        {
            terrainLoader = loaders.OfType<TerrainLoader>().FirstOrDefault();
            keyAndHoleLoader = loaders.OfType<KeyAndHoleLoader>().FirstOrDefault();
        }
    }

    public void Load()
    {
        foreach (ILoader loader in loaders)
        {
            loader.Load();
        }
        terrainExclusionController.SetExclusionCollider(true);
    }

    public void Unload()
    {
        foreach (ILoader loader in loaders)
        {
            loader?.Unload();
        }
        if (terrainExclusionController != null)
        {
            terrainExclusionController.SetExclusionCollider(false);
        }
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
        if (doorObject != null)
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
        else
        {
            throw new NullReferenceException("No door object reference found.");
        }
    }

    public Animator GetDoorAniamtor()
    {
        return doorObject.GetComponent<Animator>();
    }

    public List<Collider> GetTerrainExclusionColliders()
    {
        return terrainExclusionController.GetExclusionColliders();
    }

    public void ClearedKeyAndHole()
    {
        if (keyAndHoleLoader != null)
        {
            keyAndHoleLoader.StopKeyAndHoleTimer();
        }
    }

    public void DeactivateKeyAndHole()
    {
        if (keyAndHoleLoader != null)
        {
            keyAndHoleLoader.DeactivateSelf();
        }
    }
}
