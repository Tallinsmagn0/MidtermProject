using UnityEngine;
using UnityEngine.Events;

public class LevelManager : Singleton<LevelManager>
{
    [SerializeField] private LevelState[] levels;
    [SerializeField] private UnityEvent OnLevelChange;

    private int currLevel;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currLevel = 0;
        levels[currLevel]?.OnEnter();
        OnLevelChange?.Invoke();
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
        OnLevelChange?.Invoke();
    }

    public Terrain GetCurrentTerrain()
    {
        return levels[currLevel]?.GetLevelTerrain();
    }
}
