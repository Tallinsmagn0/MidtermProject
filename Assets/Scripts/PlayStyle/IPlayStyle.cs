using UnityEngine;

public interface IPlayStyle
{
    public ITerrainSculptStrategy CreateSculptStrategy();
    public IPlayerControlStrategy CreatePlayerControlStrategy(string playerTag);
}
