using System;
using UnityEngine;

public class TerrainSculptor : MonoBehaviour
{
    [SerializeField] private int brushSize;
    [SerializeField] private float sculptAmount;
    [SerializeField] private float sigma;

    public void RaiseTerrainAtPoint(Vector3 worldPos)
    {
        AlterTerrainAtPoint(worldPos, sculptAmount);
    }

    public void LowerTerrainAtPoint(Vector3 worldPos)
    {
        AlterTerrainAtPoint(worldPos, -sculptAmount);
    }

    public void AlterTerrainAtPoint(Vector3 worldPos, float amount)
    {
        Terrain terrain = LevelManager.Instance.GetCurrentTerrain();

        if (terrain != null)
        {
            CalculateTerrainAlterationAt(terrain, worldPos, amount);
            ResetTerrainCollider();
        } else
        {
            throw new NullReferenceException("Terrain from level manager was null");
        }
    }

    private void CalculateTerrainAlterationAt(Terrain terrain, Vector3 worldPos, float amount)
    {
        TerrainData data = terrain.terrainData;

        // Convert world position into Terrain local position
        Vector3 tempCoord = worldPos - terrain.transform.position;
        Vector3 coord;
        coord.x = tempCoord.x / data.size.x;
        coord.z = tempCoord.z / data.size.z;

        // Convert local fractional positions to heightmap matrix coordinates
        int xMap = (int)(coord.x * data.heightmapResolution);
        int zMap = (int)(coord.z * data.heightmapResolution);

        // Define the modification area bounds
        int offset = brushSize / 2;
        int startX = Mathf.Clamp(xMap - offset, 0, data.heightmapResolution - 1);
        int startZ = Mathf.Clamp(zMap - offset, 0, data.heightmapResolution - 1);
        int width = Mathf.Min(brushSize, data.heightmapResolution - startX);
        int height = Mathf.Min(brushSize, data.heightmapResolution - startZ);

        // Fetch the 2D array representing current heights
        float[,] heights = data.GetHeights(startX, startZ, width, height);

        Vector2 center = new Vector2(width / 2.0f, height / 2.0f);

        // Modify the heights inside the matrix
        for (int x = 0; x < width; x++)
        {
            for (int z = 0; z < height; z++)
            {
                float distX = x - center.x;
                float distZ = z - center.y;
                heights[z, x] += amount * Mathf.Exp(-( (distX * distX + distZ * distZ) / (2.0f * sigma * sigma)));
            }
        }

        // Apply back to the terrain
        data.SetHeights(startX, startZ, heights);
    }

    private void ResetTerrainCollider()
    {
        Terrain terrain = LevelManager.Instance.GetCurrentTerrain();
        TerrainCollider terrainCollider = terrain.GetComponent<TerrainCollider>();

        if (terrainCollider != null)
        {
            terrainCollider.terrainData = terrain.terrainData;
        }
    }
}
