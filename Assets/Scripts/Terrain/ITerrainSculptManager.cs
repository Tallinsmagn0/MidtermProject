using UnityEngine;

public interface ITerrainSculptManager
{
    public void Setup();
    public void PrecomputeTerrainAlterationAt(Vector3 worldPos, bool isRaising);
    public void PerformTerainAlteration(float time);
    public void FinishTerainAlteration();
    public void ResetTerrain();
    public float GetAlterDuration();
    public int GetBrushSize();
    public void SetBrushSize(int brushSize);
}
