using UnityEngine;
using UnityEngine.Events;

public class TerrainDataAlterBehavior : MonoBehaviour
{
    TerrainData terrainData;

    // Sculpt variables
    [SerializeField] private float sculptSize;
    [SerializeField] [Range(0f, 1f)] private float sculptDeviation;
    [SerializeField] private float alterDuration;
    [SerializeField] private UnityEvent<float> OnSculptTerrain;

    // Heightmap matrix references
    float sculptAmount;
    int startX, startZ;
    int width, height;
    float[,] startHeights, endHeights;

    public void CalculateTerrainAlteration(Vector3 worldPos, Vector3 terrainPos, int brushSize, bool isRaising)
    {
        CalculateTerrainAlterationLocationValues(worldPos, terrainPos, brushSize);
        CalculateHeightMatrixValues(brushSize, isRaising);
    }

    private void CalculateTerrainAlterationLocationValues(Vector3 worldPos, Vector3 terrainPos, int brushSize)
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

    private void CalculateHeightMatrixValues(int brushSize, bool isRaising)
    {
        // Fetch the 2D array representing current heights
        startHeights = terrainData.GetHeights(startX, startZ, width, height);
        endHeights = (float[,]) startHeights.Clone();

        Vector2 center = new Vector2(width / 2.0f, height / 2.0f);
        float sigma = sculptDeviation * brushSize;
        int amountSign = isRaising ? 1 : -1;

        // Modify the heights inside the matrix
        for (int x = 0; x < width; x++)
        {
            for (int z = 0; z < height; z++)
            {
                float distX = x + 0.5f - center.x;
                float distZ = z + 0.5f - center.y;
                float deltaAmount = amountSign * sculptSize * Mathf.Exp(-( (distX * distX + distZ * distZ) / (2.0f * sigma * sigma)));
                endHeights[z, x] = Mathf.Clamp(endHeights[z, x] + deltaAmount, 0f, 1f);
            }
        }

        // TODO: update sculptAmount with deltaAmounts
        // if sculptAmout is outside ranges, then do nothing
    }

    public void LerpTerrainData(float t)
    {
        float[,] currHeights = terrainData.GetHeights(startX, startZ, width, height);
        float[,] lerpHeights = new float[height, width];
        float sculptDelta = 0;

        for (int x = 0; x < width; x++)
        {
            for (int z = 0; z < height; z++)
            {
                lerpHeights[z, x] = Mathf.Lerp(startHeights[z, x], endHeights[z, x], t / alterDuration);
                sculptDelta += lerpHeights[z, x] - currHeights[z, x];
            }
        }
        SetDataHeightmap(lerpHeights, sculptDelta);
    }

    public void FinalizeTerrainData()
    {
        float[,] currHeights = terrainData.GetHeights(startX, startZ, width, height);
        float sculptDelta = 0;

        for (int x = 0; x < width; x++)
        {
            for (int z = 0; z < height; z++)
            {
                sculptDelta += endHeights[z, x] - currHeights[z, x];
            }
        }
        SetDataHeightmap(endHeights, sculptDelta);
    }

    private void SetDataHeightmap(float[,] heights, float sculptDelta)
    {
        terrainData.SetHeights(startX, startZ, heights);
        OnSculptTerrain?.Invoke(sculptDelta);
    }

    public void ResetTerrainData(TerrainData terrainData)
    {
        this.terrainData = terrainData;
        sculptAmount = 0;
    }

    public float GetAlterDuration()
    {
        return alterDuration;
    }
}
