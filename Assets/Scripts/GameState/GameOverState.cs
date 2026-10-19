using UnityEngine;

public class GameOverState : IGameState
{

    private bool isPopupVisible;

    public void OnEnter(GameManager gm) { }

    public void OnExit(GameManager gm)
    {
        gm.UnpauseGame();
        LevelManager.Instance.UnloadLevels();
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
}
