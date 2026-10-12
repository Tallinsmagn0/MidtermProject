using UnityEngine;

public class CutsceneManager : Singleton<CutsceneManager>
{
    [SerializeField] private KeyInHoleCutsceneController keyInHoleController;

    public void SetUpIntroCutscene()
    {
        
    }

    public void DisableIntroCutscene()
    {
        
    }

    public void SetUpPlayCutscenes()
    {
        keyInHoleController.gameObject.SetActive(true);
    }

    public void DisablePlayCutscenes()
    {
        keyInHoleController.gameObject.SetActive(false);
    }
}
