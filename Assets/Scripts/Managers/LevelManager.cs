using UnityEngine;

public class LevelManager : Singleton<LevelManager>
{
    [SerializeField]
    private LevelState[] levels;

    private int currLevel;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currLevel = 0;
        levels[currLevel]?.OnEnter();
    }

    // Update is called once per frame
    void Update()
    {
        levels[currLevel]?.Run();
    }

    public void IncrementLevel()
    {
        levels[currLevel]?.OnExit();
        currLevel++;
        levels[currLevel]?.OnEnter();
    }

    public Terrain GetCurrentTerrain()
    {
        return levels[currLevel]?.GetCurrentTerrain();
    }

    void OnDisable()
    {
        foreach (LevelState levelState in levels)
        {
            levelState.OnExit();
        }
    }
}
