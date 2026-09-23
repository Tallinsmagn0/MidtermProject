using System;
using UnityEngine;

public class TerrainSculptor : MonoBehaviour
{
    [SerializeField] private Terrain terrain;
    [SerializeField] private int brushSize;
    [SerializeField] private float sculptAmount;
    [SerializeField] private float sigma;
    [SerializeField] private float alterDuration;

    bool isAltering = false;
    float alterTime;

    int startX, startZ;
    int width, height;

    float[,] startHeights, endHeights;

    void Update()
    {
        if (isAltering && terrain != null)
        {
            if (alterTime < alterDuration)
            {
                SetTerrainDataHeightmap();
                ResetTerrainCollider();
                alterTime += Time.deltaTime;
            } 
            else
            {
                SetEndDataHeightmap();
                isAltering = false;
                alterTime = 0;
            }
        }
    }

    public void RaiseTerrainAtPoint(Vector3 worldPos)
    {
        AlterTerrainAtPoint(worldPos, sculptAmount);
    }

    public void LowerTerrainAtPoint(Vector3 worldPos)
    {
        AlterTerrainAtPoint(worldPos, -sculptAmount);
    }

    private void AlterTerrainAtPoint(Vector3 worldPos, float amount)
    {
        if (terrain != null)
        {
            if (!isAltering)
            {
                isAltering = true;
                CalculateTerrainAlterationLocationValues(terrain.transform.position, worldPos);
                CalculateHeightMatrixValues(amount);
            }
        }
        else
        {
            throw new NullReferenceException("Tried to alter null terrain");
        }
    }

    private void SetTerrainDataHeightmap()
    {
        float[,] heights = new float[height, width];

        for (int x = 0; x < width; x++)
        {
            for (int z = 0; z < height; z++)
            {
                heights[z, x] = Mathf.Lerp(startHeights[z, x], endHeights[z, x], alterTime / alterDuration);
            }
        }
        terrain.terrainData.SetHeights(startX, startZ, heights);
    }

    private void SetEndDataHeightmap()
    {
        terrain.terrainData.SetHeights(startX, startZ, endHeights);
    }

    private void CalculateTerrainAlterationLocationValues(Vector3 terrainPos, Vector3 worldPos)
    {
        TerrainData data = terrain.terrainData;

        // Convert world position into Terrain local position
        Vector3 tempCoord = worldPos - terrainPos;
        Vector3 coord;
        coord.x = tempCoord.x / data.size.x;
        coord.z = tempCoord.z / data.size.z;

        // Convert local fractional positions to heightmap matrix coordinates
        int xMap = (int)(coord.x * data.heightmapResolution);
        int zMap = (int)(coord.z * data.heightmapResolution);

        // Define the modification area bounds
        int offset = brushSize / 2;

        startX = Mathf.Clamp(xMap - offset, 0, data.heightmapResolution - 1);
        startZ = Mathf.Clamp(zMap - offset, 0, data.heightmapResolution - 1);

        width = Mathf.Min(brushSize, data.heightmapResolution - startX);
        height = Mathf.Min(brushSize, data.heightmapResolution - startZ);
    }

    private void CalculateHeightMatrixValues(float amount)
    {
        // Fetch the 2D array representing current heights
        startHeights = terrain.terrainData.GetHeights(startX, startZ, width, height);
        endHeights = (float[,]) startHeights.Clone();

        Vector2 center = new Vector2(width / 2.0f, height / 2.0f);

        // Modify the heights inside the matrix
        for (int x = 0; x < width; x++)
        {
            for (int z = 0; z < height; z++)
            {
                float distX = x + 0.5f - center.x;
                float distZ = z + 0.5f - center.y;
                endHeights[z, x] += amount * Mathf.Exp(-( (distX * distX + distZ * distZ) / (2.0f * sigma * sigma)));
            }
        }
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
