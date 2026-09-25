using UnityEngine;

public interface IGameState
{
    void Run(GameManager gm);
    void OnEnter(GameManager gm);
    void OnExit(GameManager gm);
}
