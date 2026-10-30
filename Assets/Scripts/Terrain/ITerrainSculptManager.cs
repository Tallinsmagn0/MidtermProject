using UnityEngine;

public interface ITerrainSculptStrategy
{
    public bool AssessTerrainAlterationAt(Vector3 worldPos, bool isRaising, float height);
    public void PerformTerainAlteration(float time);
    public void FinishTerainAlteration();
    public void ResetTerrain();
    public float GetAlterDuration();
    public int GetBrushSize();
    public void SetBrushSize(int brushSize);
}
