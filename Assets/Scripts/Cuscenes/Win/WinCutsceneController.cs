using UnityEngine;

public class WinCutsceneController : CutsceneController
{
    [SerializeField] private Transform playerSpawn;
    [SerializeField] private Transform sandAlterPoint;
    [SerializeField] private float terrainSculptSizeMod;

    private TerrainSculptor terrainSculptor;

    public void SetUpCutscene()
    {
        PlayerManager.Instance.SetPlayerPivot(playerSpawn);
        terrainSculptor = PlayerManager.Instance.GetPlayerTerrainSculptor();
    }

    public void RaiseSand()
    {
        terrainSculptor.RaiseTerrainAtPoint(sandAlterPoint.position, terrainSculptSizeMod);
    }

    public void LowerSand()
    {
        terrainSculptor.LowerTerrainAtPoint(sandAlterPoint.position, terrainSculptSizeMod);
    }
}
