using UnityEngine;

public abstract class EndState : IGameState
{
    private bool isPopupVisible;

    public abstract void OnEnter(GameManager gm);

    public void OnExit(GameManager gm)
    {
        gm.UnpauseGame();
        LevelManager.Instance.UnloadLevels();
        DestroyManagers();
    }

    public void Run(GameManager gm)
    {
        if (!isPopupVisible && HasFinishedEndingCutscene())
        {
            isPopupVisible = true;
            ShowPopup();
            PlayerManager.Instance.DisablePlayerControls();
            gm.PauseGame();
        }
    }

    protected abstract bool HasFinishedEndingCutscene();

    protected abstract void ShowPopup();

    private void DestroyManagers()
    {
        Object.Destroy(LevelManager.Instance.gameObject);
        Object.Destroy(PlayerManager.Instance.gameObject);
        Object.Destroy(SandManager.Instance.gameObject);
        Object.Destroy(UIManager.Instance.gameObject);
        Object.Destroy(CutsceneManager.Instance.gameObject);
    }
}
