using UnityEngine;
using UnityEngine.Events;

public class LevelManager : Singleton<LevelManager>
{
    [SerializeField] private LevelData[] levels;
    [SerializeField] private UnityEvent OnLevelChange;

    private int currLevel;

    public void LoadFirstLevel()
    {
        currLevel = 0;
        levels[currLevel]?.Load();
        OnLevelChange?.Invoke();
    }

    public void IncrementLevel()
    {
        levels[currLevel]?.Unload();
        currLevel++;
        levels[currLevel]?.Load();
        OnLevelChange?.Invoke();
    }

    public Terrain GetCurrentTerrain()
    {
        return levels[currLevel]?.GetLevelTerrain();
    }

    public float GetMaxSandAmount()
    {
        return levels[currLevel].GetMaxSandAmount();
    }
}
