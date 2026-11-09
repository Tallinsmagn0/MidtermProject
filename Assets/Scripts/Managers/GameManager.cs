using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    IGameState currState;

    void Start()
    {
        currState = new MenuState();
        currState.OnEnter(this);
    }

    void Update()
    {
        currState.Run(this);
    }

    public void Transition(IGameState newState)
    {
        currState?.OnExit(this);
        newState.OnEnter(this);
        currState = newState;
    }

    public void GameOver()
    {
        IGameState gameOverState = new GameOverState();
        Transition(gameOverState);
    }

    public void WinGame()
    {
        IGameState winState = new WinState();
        Transition(winState);
    }

    public void ReturnToMenu()
    {
        IGameState menuState = new MenuState();
        Transition(menuState);
    }

    public void StartGameplay()
    {
        IGameState playState = new PlayState();
        Transition(playState);
    }

    public void StartGame()
    {
        StartGame(false);
    }

    public void RestartGame()
    {
        StartGame(true);
    }

    public void StartGame(bool isRestart)
    {
        IGameState introState = new IntroState(isRestart);
        Transition(introState);
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
    }

    public void UnpauseGame()
    {
        Time.timeScale = 1f;
    }
}
