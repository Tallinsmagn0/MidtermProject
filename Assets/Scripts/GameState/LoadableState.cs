using UnityEngine;

public abstract class LoadableState : IGameState
{
    protected string sceneName;
    protected bool loadOnEnter;
    bool hasSetupScene = false;
    bool hasSceneLoaded = false;

    public LoadableState()
    {
        loadOnEnter = false;
    }

    public LoadableState(bool _loadOnEnter)
    {
        loadOnEnter = _loadOnEnter;
    }
    
    public void OnEnter(GameManager gm)
    {
        if (loadOnEnter)
        {
           SceneLoader.Instance.LoadSceneByName(sceneName);  
        }
        else
        {
            if (!SceneLoader.Instance.HasSceneLoaded(sceneName))
            {
            SceneLoader.Instance.LoadSceneByName(sceneName); 
            }
        }
    }

    public void Run(GameManager gm)
    {
        if (hasSetupScene)
        {
            RunScene(gm);
        }
        else
        {
            SetUpOnSceneLoad();
        }
    }

    protected abstract void RunScene(GameManager gm);

    private void SetUpOnSceneLoad()
    {
        if (!hasSceneLoaded)
        {
            hasSceneLoaded = SceneLoader.Instance.HasSceneLoaded(sceneName);
        }
        else
        {
            SetUpSceneAndVariables();
        }
    }

    private void SetUpSceneAndVariables()
    {
        SetUpScene();
        hasSetupScene = true;
    }

    protected abstract void SetUpScene();

    public abstract void OnExit(GameManager gm);

}
