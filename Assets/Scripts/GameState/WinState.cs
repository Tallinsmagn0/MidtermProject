using UnityEngine;

public class WinState : EndState
{
    
    public override void OnEnter(GameManager gm)
    {
        CutsceneManager.Instance.SetUpWinCutscene();
        CutsceneManager.Instance.RunWinCutscene();
    }

    protected override bool HasFinishedEndingCutscene()
    {
        return CutsceneManager.Instance.HasFinishedWinCutscene();
    }

    protected override void ShowPopup()
    {
        UIManager.Instance.SetWinPopupVisibility(true);
    }
}