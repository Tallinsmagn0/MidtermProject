using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    IGameState currState;
    IPlayStyle playStyle;

    void Start()
    {
        currState = new PlayState();
        playStyle = new SoloPlayStyle();
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

    public IPlayStyle GetPlayStyle()
    {
        return playStyle;
    }
}
