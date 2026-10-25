using UnityEngine;

public class EndButtonBehaviour : MonoBehaviour
{
    public void QuitGame()
    {
        GameManager.Instance.ReturnToMenu();
        SceneLoader.Instance.LoadSceneByName("MenuScene");
    }

    public void RestartGame()
    {
        GameManager.Instance.RestartGame();
    }
}
