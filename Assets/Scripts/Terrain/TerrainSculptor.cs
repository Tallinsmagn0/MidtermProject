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
        sculptStrategy = PlayStyleManager.Instance?.PlayStyle?.CreateSculptStrategy();
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
        bool canAlterAt = sculptStrategy.AssessTerrainAlterationAt(worldPos, isRaising);
        ResetAlterStatus(canAlterAt);
    }

    private void UpdateTerrainAlter()
    {
        sculptStrategy.PerformTerainAlteration(alterTime);
        alterTime += Time.deltaTime;
    }

    private void FinishTerrainAlter()
    {
        sculptStrategy.FinishTerainAlteration();
        ResetAlterStatus(false);
    }

    private void ResetAlterStatus(bool isAltering)
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
        ResetAlterStatus(false);
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
