using UnityEngine;

public class TerrainDataAlterBehavior : MonoBehaviour
{
    TerrainData terrainData;

    // Sculpt variables
    [SerializeField] private float sculptAmount;
    [SerializeField] private float sigma;

    // Heightmap matrix references
    int startX, startZ;
    int width, height;
    float[,] startHeights, endHeights;

    public void CalculateTerrainAlteration(Vector3 worldPos, Vector3 terrainPos, int brushSize, bool isRaising)
    {
        CalculateTerrainAlterationLocationValues(worldPos, terrainPos, brushSize);
        CalculateHeightMatrixValues(isRaising);
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

    private void CalculateHeightMatrixValues(bool isRaising)
    {
        // Fetch the 2D array representing current heights
        startHeights = terrainData.GetHeights(startX, startZ, width, height);
        endHeights = (float[,]) startHeights.Clone();

        Vector2 center = new Vector2(width / 2.0f, height / 2.0f);
        int amountSign = isRaising ? 1 : -1;

        // Modify the heights inside the matrix
        for (int x = 0; x < width; x++)
        {
            for (int z = 0; z < height; z++)
            {
                float distX = x + 0.5f - center.x;
                float distZ = z + 0.5f - center.y;
                endHeights[z, x] += amountSign * sculptAmount * Mathf.Exp(-( (distX * distX + distZ * distZ) / (2.0f * sigma * sigma)));
            }
        }
    }

    public void LerpTerrainData(float t)
    {
        float[,] heights = new float[height, width];

        for (int x = 0; x < width; x++)
        {
            for (int z = 0; z < height; z++)
            {
                heights[z, x] = Mathf.Lerp(startHeights[z, x], endHeights[z, x], t);
            }
        }
        terrainData.SetHeights(startX, startZ, heights);
    }

    public void FinalizeTerrainData()
    {
        terrainData.SetHeights(startX, startZ, endHeights);
    }

    public void SetTerrainData(TerrainData terrainData)
    {
        this.terrainData = terrainData;
    }
}
