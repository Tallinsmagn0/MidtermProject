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

    public bool AssessTerrainAlterationAt(Vector3 worldPos, bool isRaising)
    {
        if (terrain != null)
        {
            return dataAlterBehavior.CalculateTerrainDataAlter(worldPos, terrain.transform.position, brushBehavior.GetBrushSize(), isRaising);
        }
        else
        {
            throw new NullReferenceException("Tried to alter null terrain");
        }
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
