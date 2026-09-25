using UnityEngine;

public class SandController : MonoBehaviour
{
    [SerializeField] private TerrainSculptor terrainSculptor;
    [SerializeField] private SandAmountBehavior sandAmountBehavior;

    public void RaiseSandAt(Vector3 worldPos)
    {
        if (!sandAmountBehavior.IsEmpty())
        {
            terrainSculptor.RaiseTerrainAtPoint(worldPos);
        }
    }

    public void LowerSandAt(Vector3 worldPos)
    {
        if (!sandAmountBehavior.IsFull())
        {
            terrainSculptor.LowerTerrainAtPoint(worldPos);
        }
    }

    public void ResetSand()
    {
        terrainSculptor.ResetCurrentTerrain();
        sandAmountBehavior.ResetMaxSandAmount();
    }
}
