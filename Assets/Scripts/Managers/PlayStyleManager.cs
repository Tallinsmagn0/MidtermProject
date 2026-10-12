using UnityEngine;

public class PlayStyleManager : Singleton<PlayStyleManager>
{
    [SerializeField] private string playerTag;

    private IPlayStyle _playStyle;

    public IPlayStyle PlayStyle
    {
        get
        {
            return _playStyle;
        }
        set
        {
            _playStyle = value;
        }
    }

    public void SetPlayStyleFromScene()
    {
        if (GameObject.FindGameObjectsWithTag(playerTag).Length == 1)
        {
            PlayStyle = new SoloPlayStyle();
        }
        else
        {
            throw new UnityException("Cannot create playstyle for the given number of players");
        }
    }
}
