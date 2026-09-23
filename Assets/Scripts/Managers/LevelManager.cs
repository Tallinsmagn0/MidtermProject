using UnityEngine;
using UnityEngine.Events;

public class LevelManager : Singleton<LevelManager>
{
    [SerializeField]
    private LevelState[] levels;

    [SerializeField]
    private Terrain currentTerrain;

    private int currLevel;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currLevel = 0;
        levels[currLevel]?.OnEnter(this);
    }

    // Update is called once per frame
    void Update()
    {
        levels[currLevel]?.Run();
    }

    public void IncrementLevel()
    {
        levels[currLevel]?.OnExit(this);
        currLevel++;
        levels[currLevel]?.OnEnter(this);
    }

    public Terrain GetCurrentTerrain()
    {
        return currentTerrain;
    }

    void OnDisable()
    {
        levels[currLevel].OnExit(this);
    }
}
