using UnityEngine;

public class WinCutsceneController : CutsceneController
{
    [SerializeField] private Transform playerSpawn;

    public void SetUpCutscene()
    {
        PlayerManager.Instance.SetPlayerPivot(playerSpawn);
    }
}
