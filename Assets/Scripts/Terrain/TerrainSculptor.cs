using UnityEngine;

public class TerrainSculptor : MonoBehaviour
{   
    private ITerrainSculptStrategy sculptStrategy;

    // Alter variables
    bool isAltering = false;
    float alterTime;

    void Start()
    {
        SetupSculptStrategy();
    }

    private void SetupSculptStrategy()
    {
        sculptStrategy = GameManager.Instance.GetPlayStyle()?.CreateSculptStrategy();
    }

    void Update()
    {
        if (isAltering)
        {
            if (alterTime < sculptStrategy.GetAlterDuration())
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
        if (!isAltering)
        {
            StartTerrainAlter(worldPos, isRaising);
        }
    }

    private void StartTerrainAlter(Vector3 worldPos, bool isRaising)
    {
        sculptStrategy.PrecomputeTerrainAlterationAt(worldPos, isRaising);
        ResetAlterTimer(true);
    }

    private void UpdateTerrainAlter()
    {
        sculptStrategy.PerformTerainAlteration(alterTime);
        alterTime += Time.deltaTime;
    }

    private void FinishTerrainAlter()
    {
        sculptStrategy.FinishTerainAlteration();
        ResetAlterTimer(false);
    }

    private void ResetAlterTimer(bool isAltering)
    {
        this.isAltering = isAltering;
        alterTime = 0;
    }

    public void ResetCurrentTerrain()
    {
        if (sculptStrategy == null)
        {
            SetupSculptStrategy();
        }
        sculptStrategy.ResetTerrain();
        ResetAlterTimer(false);
    }

    public bool IsAlteringTerrain()
    {
        return isAltering;
    }

    public int GetBrushSize()
    {
        return sculptStrategy.GetBrushSize();
    }

    public void SetBrushSize(int brushSize)
    {
        sculptStrategy.SetBrushSize(brushSize);
    }
}
