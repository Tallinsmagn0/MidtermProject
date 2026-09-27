using System;
using System.Linq;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    [SerializeField] [Range(0f, 1f)] private float maxSandAmount;
    [SerializeField] private ILoader[] loaders;

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
}
