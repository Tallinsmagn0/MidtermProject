using UnityEngine;

public interface IPlayerControlStrategy
{
    public void SetControlsEnabled(bool isEnabled);

    public void SetPivot(Transform pivot);
}
