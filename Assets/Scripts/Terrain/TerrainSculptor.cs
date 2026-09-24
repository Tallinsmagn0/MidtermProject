using System;
using UnityEngine;

public class TerrainSculptor : MonoBehaviour
{
    // Terrain references
    private Terrain currentTerrain;
    [SerializeField] private TerrainSculptBrushBehavior brushBehavior;
    [SerializeField] private TerrainDataAlterBehavior dataAlterBehavior;

    // Alter variables
    bool isAltering = false;
    float alterTime;

    void Update()
    {
        if (isAltering)
        {
            if (alterTime < dataAlterBehavior.GetAlterDuration())
            {
                UpdateTerrainAlter();
            } 
            else
            {
                FinishTerrainAlter();
            }
        }
    }

    public void RaiseTerrainAtPoint(Vector3 worldPos)
    {
        AlterTerrainAtPoint(worldPos, true);
    }

    public void LowerTerrainAtPoint(Vector3 worldPos)
    {
        AlterTerrainAtPoint(worldPos, false);
    }

    private void AlterTerrainAtPoint(Vector3 worldPos, bool isRaising)
    {

        if (currentTerrain != null)
        {
            if (!isAltering)
            {
                StartTerrainAlter(worldPos, isRaising);
            }
        }
        else
        {
            throw new NullReferenceException("Tried to alter null terrain");
        }
    }

    private void StartTerrainAlter(Vector3 worldPos, bool isRaising)
    {
        dataAlterBehavior.CalculateTerrainAlteration(worldPos, currentTerrain.transform.position, brushBehavior.GetBrushSize(), isRaising);
        ResetAlterTimer(true);
    }

    private void UpdateTerrainAlter()
    {
        dataAlterBehavior.LerpTerrainData(alterTime);
        ResetTerrainCollider();
        alterTime += Time.deltaTime;
    }

    private void FinishTerrainAlter()
    {
        dataAlterBehavior.FinalizeTerrainData();
        ResetAlterTimer(false);
    }

    private void ResetAlterTimer(bool isAltering)
    {
        this.isAltering = isAltering;
        alterTime = 0;
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
        currentTerrain = LevelManager.Instance.GetCurrentTerrain();

        if (currentTerrain != null)
        {
            dataAlterBehavior.SetTerrainData(currentTerrain.terrainData);
        }
        ResetAlterTimer(false);
    }

    public bool IsAlteringTerrain()
    {
        return isAltering;
    }

    public int GetBrushSize()
    {
        return brushBehavior.GetBrushSize();
    }

    public void SetBrushSize(int newSize)
    {
        brushBehavior.SetBrushSize(newSize);
    }
}
