using System;
using UnityEngine;

public class SoloTerrainSculptManager : ITerrainSculptManager
{
    private Terrain terrain;
    private TerrainSculptBrushBehavior brushBehavior;
    private TerrainDataAlterBehavior dataAlterBehavior;

    public void Setup()
    {
        brushBehavior = UnityEngine.Object.FindFirstObjectByType<TerrainSculptBrushBehavior>();
        dataAlterBehavior = UnityEngine.Object.FindFirstObjectByType<TerrainDataAlterBehavior>();
    }

    public void PrecomputeTerrainAlterationAt(Vector3 worldPos, bool isRaising)
    {
        if (terrain != null)
        {
            dataAlterBehavior.CalculateTerrainAlteration(worldPos, terrain.transform.position, brushBehavior.GetBrushSize(), isRaising);
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
            dataAlterBehavior.ResetTerrainData(terrain.terrainData);
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
