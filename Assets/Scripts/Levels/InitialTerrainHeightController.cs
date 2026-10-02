using UnityEngine;
using System.Collections.Generic;

public class InitialTerrainHeightController : MonoBehaviour
{
    [SerializeField] private Terrain terrain;

    [SerializeField] List<Collider> alterationColliders;

    [SerializeField] [Range(0f, 1f)] float initialHeight;

    public void SetInitialHeights()
    {
        TerrainData data = terrain.terrainData;
        bool[,] alterPoints = TerrainDataFunctions.GetTerrainOverlapsWithColliders(terrain, alterationColliders);
        float[,] alteredHeights = new float[data.heightmapResolution, data.heightmapResolution];

        for (int x = 0; x < data.heightmapResolution; x++)
        {
            for (int z = 0; z < data.heightmapResolution; z++)
            {
                if (alterPoints[z, x])
                {
                    alteredHeights[z, x] = Mathf.Clamp(initialHeight, 0f, 1f);
                }
            }
        }
        data.SetHeights(0, 0, alteredHeights);

        foreach (Collider collider in alterationColliders)
        {
            collider.transform.Translate(Vector3.up * initialHeight * data.size.y);
        }
    }
}
