using UnityEngine;

public class SoloPlayStyle : IPlayStyle
{
    public IPlayerControlStrategy CreatePlayerControlStrategy(string playerTag)
    {
        return new SoloPlayerControlStrategy(playerTag);
    }

    public ITerrainSculptStrategy CreateSculptStrategy()
    {
        return new SoloTerrainSculptStrategy();
    }
}
