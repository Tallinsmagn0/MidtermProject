using System;
using UnityEngine;

public class SoloPlayerControlStrategy : IPlayerControlStrategy
{
    private string playerTag;

    public SoloPlayerControlStrategy(string _playerTag)
    {
        playerTag = _playerTag;
    }

    public void SetControlsEnabled(bool isEnabled)
    {
        GameObject[] playerObjects = GameObject.FindGameObjectsWithTag(playerTag);

        if (playerObjects.Length == 1)
        {
            GameObject playerObject = playerObjects[0];
            PlayerControlsController controller = playerObject.GetComponent<PlayerControlsController>();

            if (controller != null)
            {
                controller.SetControlsEnbaled(isEnabled);
            }
            else
            {
                throw new NullReferenceException("No PlayerControlsController component found on player object");
            }
        } 
        else
        {
            throw new UnityException("More than one player object in scene");
        }
    }
}
