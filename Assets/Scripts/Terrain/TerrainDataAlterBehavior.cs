using UnityEngine;
using UnityEngine.Events;

public class TerrainDataAlterBehavior : MonoBehaviour
{
    TerrainData terrainData;

    // Sculpt variables
    [SerializeField] private float sculptSize;
    [SerializeField] [Range(0f, 1f)] private float sculptDeviation;
    [SerializeField] private float sculptDuration;
    [SerializeField] private UnityEvent<float> OnSculptTerrain;

    // Heightmap matrix references
    float sculptAmount;
    int startX, startZ = -1;
    int width, height = 0;
    float[,] startHeights, endHeights;

    public bool CalculateTerrainDataAlter(Vector3 worldPos, Vector3 terrainPos, int brushSize, bool isRaising)
    {
        CalculateTerrainDataAlterVariables(worldPos, terrainPos, brushSize);
        float finalSculptAmount = CalculateSculptAmountOnMatrix(brushSize, isRaising);
        bool canSculptTerrain = CanSculptTerrainByAmount(finalSculptAmount);

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
                float distX = x + 0.5f - center.x;
                float distZ = z + 0.5f - center.y;
                float deltaAmount = amountSign * sculptSize * Mathf.Exp(-( (distX * distX + distZ * distZ) / (2.0f * sigma * sigma)));
                float endHeight = startHeights[z, x] + deltaAmount;

                tempSculptAmount += deltaAmount;
                endHeights[z, x] = Mathf.Clamp(endHeight, 0f, 1f);
            }
        }
        return tempSculptAmount;
    }

    private bool CanSculptTerrainByAmount(float tempSculptAmount)
    {
        return SandManager.Instance.CanSculptMoreTerrain(tempSculptAmount);
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

    public void ResetTerrainData(TerrainData terrainData)
    {
        this.terrainData = terrainData;
        sculptAmount = 0;
        ResetSculptVariables();
    }

    public float GetAlterDuration()
    {
        return sculptDuration;
    }
}
