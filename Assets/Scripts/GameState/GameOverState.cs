using UnityEngine;

public class GameOverState : IGameState
{

    private bool isPopupVisible;

    public void OnEnter(GameManager gm)
    {
        CutsceneManager.Instance.ActivateGameOverCutscene();
        CutsceneManager.Instance.RunGameOverCutscene();
    }

    public void OnExit(GameManager gm)
    {
        gm.UnpauseGame();
        LevelManager.Instance.UnloadLevels();
        DestroyManagers();
    }

    public void Run(GameManager gm)
    {
        if (!isPopupVisible && CutsceneManager.Instance.HasFinishedGameOverCutscene())
        {
            isPopupVisible = true;
            UIManager.Instance.SetGameOverPopupVisibility(true);
            PlayerManager.Instance.DisablePlayerControls();
            gm.PauseGame();
        }
    }

    private void DestroyManagers()
    {
        Object.Destroy(LevelManager.Instance.gameObject);
        Object.Destroy(PlayerManager.Instance.gameObject);
        Object.Destroy(SandManager.Instance.gameObject);
        Object.Destroy(UIManager.Instance.gameObject);
        Object.Destroy(CutsceneManager.Instance.gameObject);
    }
}
