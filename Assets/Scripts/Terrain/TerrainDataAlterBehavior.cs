using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;

using System.Text;

public class TerrainDataAlterBehavior : MonoBehaviour
{
    TerrainData terrainData;

    // Sculpt variables
    [SerializeField] private float sculptSize;
    [SerializeField] [Range(0f, 1f)] private float sculptDeviation;
    [SerializeField] private float sculptDuration;
    [SerializeField] private UnityEvent<float> OnSculptTerrain;

    // Persistent heightmap matrix references
    float sculptAmount;
    bool[,] heightmapExclusions;

    // Per-sculpt heightmap matrix references
    int startX, startZ = -1;
    int width, height = 0;
    float[,] startHeights, endHeights;

    public bool CalculateTerrainDataAlter(Vector3 worldPos, Vector3 terrainPos, int brushSize, bool isRaising)
    {
        CalculateTerrainDataAlterVariables(worldPos, terrainPos, brushSize);
        float finalSculptAmount = CalculateSculptAmountOnMatrix(brushSize, isRaising);
        bool canSculptTerrain = CanSculptTerrainByAmount(finalSculptAmount, isRaising);

        if (!canSculptTerrain)
        {
            ResetSculptVariables();
        }
        return canSculptTerrain;
    }

    private void CalculateTerrainDataAlterVariables(Vector3 worldPos, Vector3 terrainPos, int brushSize)
    {
        // Convert world position into Terrain local position
        Vector3 tempCoord = worldPos - terrainPos;
        Vector3 coord;
        coord.x = tempCoord.x / terrainData.size.x;
        coord.z = tempCoord.z / terrainData.size.z;

        // Convert local fractional positions to heightmap matrix coordinates
        int xMap = (int)(coord.x * terrainData.heightmapResolution);
        int zMap = (int)(coord.z * terrainData.heightmapResolution);

        // Define the modification area bound
        int offset = brushSize / 2;

        startX = Mathf.Clamp(xMap - offset, 0, terrainData.heightmapResolution - 1);
        startZ = Mathf.Clamp(zMap - offset, 0, terrainData.heightmapResolution - 1);

        width = Mathf.Min(brushSize, terrainData.heightmapResolution - startX);
        height = Mathf.Min(brushSize, terrainData.heightmapResolution - startZ);
    }

    private float CalculateSculptAmountOnMatrix(int brushSize, bool isRaising)
    {
        // Fetch the 2D array representing current heights
        startHeights = terrainData.GetHeights(startX, startZ, width, height);
        endHeights = (float[,]) startHeights.Clone();

        Vector2 center = new Vector2(width / 2.0f, height / 2.0f);
        float sigma = sculptDeviation * brushSize;
        int amountSign = isRaising ? 1 : -1;

        float tempSculptAmount = sculptAmount;

        // Modify the heights inside the matrix
        for (int x = 0; x < width; x++)
        {
            for (int z = 0; z < height; z++)
            {
                if (!heightmapExclusions[startZ + z, startX + x])
                {
                    float distX = x + 0.5f - center.x;
                    float distZ = z + 0.5f - center.y;
                    float deltaAmount = amountSign * sculptSize * Mathf.Exp(-( (distX * distX + distZ * distZ) / (2.0f * sigma * sigma)));
                    float endHeight = startHeights[z, x] + deltaAmount;

                    tempSculptAmount += deltaAmount;
                    endHeights[z, x] = Mathf.Clamp(endHeight, 0f, 1f);
                }
            }
        }
        return tempSculptAmount;
    }

    private bool CanSculptTerrainByAmount(float tempSculptAmount, bool isRaising)
    {
        if (isRaising)
        {
            float maxSculpt = SandManager.Instance.GetMaxTerrainSculptAmount();

            return tempSculptAmount < maxSculpt;
        } 
        else
        {
            return sculptAmount > 0;
        }
    }

    private void ResetSculptVariables()
    {
        startX = -1;
        startZ = -1;
        height = 0;
        width = 0;
        startHeights = null;
        endHeights = null;
    }

    private void ResetHeightmapExclusions(Terrain terrain)
    {
        List<Collider> exclusionColliders = LevelManager.Instance.GetTerrainExclusionColliders();
        heightmapExclusions = new bool[terrainData.heightmapResolution, terrainData.heightmapResolution];

        for (int x = 0; x < terrainData.heightmapResolution; x++)
        {
            for (int z = 0; z < terrainData.heightmapResolution; z++)
            {
                Vector3 worldPos = HeightmapCoordToWorldPos(terrain.GetPosition(), x, z);

                foreach (Collider collider in exclusionColliders)
                {
                    if (collider.ClosestPoint(worldPos) == worldPos)
                    {
                        heightmapExclusions[z, x] = true;
                        break;
                    }
                }
            }
        }
    }

    private Vector3 HeightmapCoordToWorldPos(Vector3 terrainPos, int mapX, int mapZ)
    {
        float normX = ((float) mapX) / (terrainData.heightmapResolution - 1);
        float normZ = ((float) mapZ) / (terrainData.heightmapResolution - 1);

        Vector3 localPos = new Vector3(normX * terrainData.size.x, terrainData.GetHeight(mapX, mapZ), normZ * terrainData.size.z);
        return localPos + terrainPos;
    }

    public void LerpTerrainData(float t)
    {
        float[,] currHeights = terrainData.GetHeights(startX, startZ, width, height);
        float[,] lerpHeights = new float[height, width];

        for (int x = 0; x < width; x++)
        {
            for (int z = 0; z < height; z++)
            {
                lerpHeights[z, x] = Mathf.Lerp(startHeights[z, x], endHeights[z, x], t / sculptDuration);
                sculptAmount += lerpHeights[z, x] - currHeights[z, x];
            }
        }
        SculptTerrain(lerpHeights);
    }

    public void FinalizeTerrainData()
    {
        float[,] currHeights = terrainData.GetHeights(startX, startZ, width, height);

        for (int x = 0; x < width; x++)
        {
            for (int z = 0; z < height; z++)
            {
                sculptAmount += endHeights[z, x] - currHeights[z, x];
            }
        }
        SculptTerrain(endHeights);
        ResetSculptVariables();
    }

    private void SculptTerrain(float[,] heights)
    {
        SetDataHeightmap(heights);
        OnSculptTerrain?.Invoke(sculptAmount);
    }

    private void SetDataHeightmap(float[,] heights)
    {
        terrainData.SetHeights(startX, startZ, heights);
    }

    public void ResetTerrainData(Terrain terrain)
    {
        terrainData = terrain.terrainData;
        sculptAmount = 0;
        ResetSculptVariables();
        ResetHeightmapExclusions(terrain);
    }

    public float GetAlterDuration()
    {
        return sculptDuration;
    }
}
