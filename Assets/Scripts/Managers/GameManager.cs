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
}
