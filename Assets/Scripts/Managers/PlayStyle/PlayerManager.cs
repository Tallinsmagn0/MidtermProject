using UnityEngine;

public class PlayerManager : Singleton<PlayerManager>
{
    [SerializeField] private string playerTag;

    private IPlayStyle _playStyle;

    private IPlayerControlStrategy controlStrategy;

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
            controlStrategy = PlayStyle.CreatePlayerControlStrategy(playerTag);
        }
        else
        {
            throw new UnityException("Cannot create playstyle for the given number of players");
        }
    }

    public void EnablePlayerControls()
    {
        SetEnablePlayerControls(true);
    }

    public void DisablePlayerControls()
    {
        SetEnablePlayerControls(false);
    }

    private void SetEnablePlayerControls(bool isEnabled)
    {
        controlStrategy.SetControlsEnabled(isEnabled);
    }
}
