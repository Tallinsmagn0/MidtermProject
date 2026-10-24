using System;
using UnityEngine;

public class SoloPlayerControlStrategy : IPlayerControlStrategy
{
    private string playerTag;

    private GameObject playerObject;

    public SoloPlayerControlStrategy(string _playerTag)
    {
        playerTag = _playerTag;

        RetrievePlayerObject();
    }

    public void SetControlsEnabled(bool isEnabled)
    {
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

    private void RetrievePlayerObject()
    {
        GameObject[] playerObjects = GameObject.FindGameObjectsWithTag(playerTag);

        if (playerObjects.Length == 1)
        {
            playerObject = playerObjects[0];
        } 
        else
        {
            throw new UnityException("More than one player object in scene");
        }
    }
}
