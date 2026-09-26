using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : Singleton<MonoBehaviour>
{
    public void LoadSceneByName(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
