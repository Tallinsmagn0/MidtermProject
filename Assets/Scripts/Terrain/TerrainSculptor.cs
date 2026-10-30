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
        sculptStrategy = PlayerManager.Instance?.PlayStyle?.CreateSculptStrategy();
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
        RaiseTerrainAtPoint(worldPos, 1f);
    }

    public void LowerTerrainAtPoint(Vector3 worldPos)
    {
        LowerTerrainAtPoint(worldPos, 1f);
    }

    public void RaiseTerrainAtPoint(Vector3 worldPos, float SculptSizeMod)
    {
        AlterTerrainAtPoint(worldPos, true, SculptSizeMod);
    }

    public void LowerTerrainAtPoint(Vector3 worldPos, float sculptSizeMod)
    {
        AlterTerrainAtPoint(worldPos, false, sculptSizeMod);
    }

    private void AlterTerrainAtPoint(Vector3 worldPos, bool isRaising, float sculptSizeMod)
    {
        if (!isAltering)
        {
            StartTerrainAlter(worldPos, isRaising, sculptSizeMod);
        }
    }

    private void StartTerrainAlter(Vector3 worldPos, bool isRaising, float sculptSizeMod)
    {
        bool canAlterAt = sculptStrategy.AssessTerrainAlterationAt(worldPos, isRaising, sculptSizeMod);
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
