using UnityEngine;
using System.Collections.Generic;

public class TerrainDataFunctions
{
    public static Vector3 HeightmapCoordToWorldPos(TerrainData data, Vector3 terrainPos, int mapX, int mapZ)
    {
        float normX = ((float) mapX) / (data.heightmapResolution - 1);
        float normZ = ((float) mapZ) / (data.heightmapResolution - 1);

        Vector3 localPos = new Vector3(normX * data.size.x, data.GetHeight(mapX, mapZ), normZ * data.size.z);
        return localPos + terrainPos;
    }

    public static bool[,] GetTerrainOverlapsWithColliders(Terrain terrain, List<Collider> colliders)
    {
        TerrainData data = terrain.terrainData;
        bool[,] overlaps = new bool[data.heightmapResolution, data.heightmapResolution];

        for (int x = 0; x < data.heightmapResolution; x++)
        {
            for (int z = 0; z < data.heightmapResolution; z++)
            {
                Vector3 worldPos = HeightmapCoordToWorldPos(data, terrain.GetPosition(), x, z);

                foreach (Collider collider in colliders)
                {
                    if (collider.ClosestPoint(worldPos) == worldPos)
                    {
                        overlaps[z, x] = true;
                        break;
                    }
                }
            }
        }
        return overlaps;
    }
}
