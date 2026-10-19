using UnityEngine;

public class GameOverButtonBehaviour : MonoBehaviour
{
    public void QuitGame()
    {
        GameManager.Instance.ReturnToMenu();
        SceneLoader.Instance.LoadSceneByName("MenuScene");
    }

    public void RestartGame()
    {
        
    }
}
