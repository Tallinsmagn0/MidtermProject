using System;
using UnityEngine;
using UnityEngine.Events;

public class TerrainSculptor : MonoBehaviour
{
    // Terrain references
    private Terrain currentTerrain;

    // Brush variables and references
    [SerializeField] private int maxBrushSize;
    [SerializeField] private int minBrushSize;
    [SerializeField] private UnityEvent<int> OnBrushSizeChange;
    private int brushSize = 5;

    // Sculpt variables
    [SerializeField] private float sculptAmount;
    [SerializeField] private float sigma;

    // Altering variables and references
    [SerializeField] private float alterDuration;
    bool isAltering = false;
    float alterTime;

    // Heightmap matrix references
    int startX, startZ;
    int width, height;

    float[,] startHeights, endHeights;

    void Update()
    {
        if (isAltering)
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
                ResetAlterTimer(false);
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

        if (currentTerrain != null)
        {
            if (!isAltering)
            {
                ResetAlterTimer(true);
                CalculateTerrainAlterationLocationValues(worldPos);
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
        currentTerrain.terrainData.SetHeights(startX, startZ, heights);
    }

    private void SetEndDataHeightmap()
    {
        currentTerrain.terrainData.SetHeights(startX, startZ, endHeights);
    }

    private void CalculateTerrainAlterationLocationValues(Vector3 worldPos)
    {
        TerrainData data = currentTerrain.terrainData;

        // Convert world position into Terrain local position
        Vector3 tempCoord = worldPos - currentTerrain.transform.position;
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
        startHeights = currentTerrain.terrainData.GetHeights(startX, startZ, width, height);
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
        TerrainCollider terrainCollider = currentTerrain.GetComponent<TerrainCollider>();

        if (terrainCollider != null)
        {
            terrainCollider.terrainData = currentTerrain.terrainData;
        }
    }

    public void ResetCurrentTerrain()
    {
        this.currentTerrain = LevelManager.Instance.GetCurrentTerrain();
        ResetAlterTimer(false);
    }

    private void ResetAlterTimer(bool isAltering)
    {
        this.isAltering = isAltering;
        alterTime = 0;
    }

    public bool IsAlteringTerrain()
    {
        return isAltering;
    }

    public int GetBrushSize()
    {
        return brushSize;
    }

    public void SetBrushSize(int newSize)
    {
        brushSize = Mathf.Clamp(newSize, minBrushSize, maxBrushSize);
        OnBrushSizeChange?.Invoke(brushSize);
    }
}
