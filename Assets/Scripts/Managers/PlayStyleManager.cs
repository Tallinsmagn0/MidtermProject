using UnityEngine;

public class PlayStyleManager : Singleton<PlayStyleManager>
{
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
}
