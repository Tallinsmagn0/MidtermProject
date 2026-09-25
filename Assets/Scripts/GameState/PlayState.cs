using UnityEngine;

public class PlayState : IGameState
{
    public void OnEnter(GameManager gm)
    {
        // LOAD TERRAINS AND FIRST LEVEL
        LevelManager.Instance.LoadFirstLevel();
    }

    public void OnExit(GameManager gm)
    {
       // UNLOAD LEVELS AND RESET TERRAINS
    }

    public void Run(GameManager gm)
    {
        
    }
}
