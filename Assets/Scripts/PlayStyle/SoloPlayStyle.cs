using UnityEngine;

public class SoloPlayStyle : IPlayStyle
{
    public ITerrainSculptStrategy CreateSculptStrategy()
    {
        return new SoloTerrainSculptStrategy();
    }
}
