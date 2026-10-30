using System;
using UnityEngine;

public class SoloTerrainSculptStrategy : ITerrainSculptStrategy
{
    private Terrain terrain;
    private TerrainSculptBrushBehavior brushBehavior;
    private TerrainDataAlterBehavior dataAlterBehavior;

    public SoloTerrainSculptStrategy()
    {
        terrain = LevelManager.Instance.GetCurrentTerrain();
        brushBehavior = UnityEngine.Object.FindFirstObjectByType<TerrainSculptBrushBehavior>();
        dataAlterBehavior = UnityEngine.Object.FindFirstObjectByType<TerrainDataAlterBehavior>();
    }

    public bool AssessTerrainAlterationAt(Vector3 worldPos, bool isRaising, float sculptSizeMod)
    {
        if (terrain != null)
        {
            if (IsPointInsideTerrain(worldPos))
            {
                return dataAlterBehavior.CalculateTerrainDataAlter(worldPos, terrain.transform.position, brushBehavior.GetBrushSize(), isRaising, sculptSizeMod);
            }
            return false;
        }
        else
        {
            throw new NullReferenceException("Tried to alter null terrain");
        }
    }

    private bool IsPointInsideTerrain(Vector3 worldPos)
    {
        if (terrain.terrainData == null) return false;

        Vector3 terrainPos = terrain.transform.position;
        Vector3 terrainSize = terrain.terrainData.size;

        if (worldPos.x < terrainPos.x || worldPos.x > terrainPos.x + terrainSize.x ||
            worldPos.z < terrainPos.z || worldPos.z > terrainPos.z + terrainSize.z)
        {
            return false;
        }
        float surfaceHeight = terrain.SampleHeight(worldPos) + terrainPos.y;
        return worldPos.y <= surfaceHeight;
    }

    public void PerformTerainAlteration(float time)
    {
        dataAlterBehavior.LerpTerrainData(time);
        ResetTerrainCollider();
    }

    private void ResetTerrainCollider()
    {
        TerrainCollider terrainCollider = terrain.GetComponent<TerrainCollider>();

        if (terrainCollider != null)
        {
            terrainCollider.terrainData = terrain.terrainData;
        }
    }

    public void FinishTerainAlteration()
    {
        dataAlterBehavior.FinalizeTerrainData();
    }

    public void ResetTerrain()
    {
        terrain = LevelManager.Instance.GetCurrentTerrain();

        if (terrain != null)
        {
            dataAlterBehavior.ResetTerrainData(terrain);
        }
    }

    public float GetAlterDuration()
    {
        return dataAlterBehavior.GetAlterDuration();
    }
    public int GetBrushSize()
    {
        return brushBehavior.GetBrushSize();
    }
    public void SetBrushSize(int brushSize)
    {
        brushBehavior.SetBrushSize(brushSize);
    }
}
