using UnityEngine;

public class TerrainSculptor : MonoBehaviour
{   
    private ITerrainSculptManager sculptManager;

    // Alter variables
    bool isAltering = false;
    float alterTime;

    void Start()
    {
        sculptManager.Setup();
    }

    void Update()
    {
        if (isAltering)
        {
            if (alterTime < sculptManager.GetAlterDuration())
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
        sculptManager.PrecomputeTerrainAlterationAt(worldPos, isRaising);
        ResetAlterTimer(true);
    }

    private void UpdateTerrainAlter()
    {
        sculptManager.PerformTerainAlteration(alterTime);
        alterTime += Time.deltaTime;
    }

    private void FinishTerrainAlter()
    {
        sculptManager.FinishTerainAlteration();
        ResetAlterTimer(false);
    }

    private void ResetAlterTimer(bool isAltering)
    {
        this.isAltering = isAltering;
        alterTime = 0;
    }

    public void ResetCurrentTerrain()
    {
        sculptManager.ResetTerrain();
        ResetAlterTimer(false);
    }

    public bool IsAlteringTerrain()
    {
        return isAltering;
    }

    public int GetBrushSize()
    {
        return sculptManager.GetBrushSize();
    }

    public void SetBrushSize(int brushSize)
    {
        sculptManager.SetBrushSize(brushSize);
    }
}
