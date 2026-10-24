using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    IGameState currState;

    void Start()
    {
        currState = new IntroState();
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

    public void RestartGame()
    {
        RestartGame(false);
    }

    public void RestartGame(bool loadOnEnter)
    {
        IGameState introState = new IntroState(loadOnEnter);
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
