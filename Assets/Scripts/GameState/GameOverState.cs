using UnityEngine;

public class GameOverState : EndState
{
    public override void OnEnter(GameManager gm)
    {
        CutsceneManager.Instance.ActivateGameOverCutscene();
        CutsceneManager.Instance.RunGameOverCutscene();
        AudioManager.Instance.PlayGameOverMusic();
    }

    protected override bool HasFinishedEndingCutscene()
    {
        return CutsceneManager.Instance.HasFinishedGameOverCutscene();
    }

    protected override void ShowPopup()
    {
        UIManager.Instance.SetUpGameOverPopup();
    }
}
