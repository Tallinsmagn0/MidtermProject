using UnityEngine;

public class DoorSoundBehavior : MonoBehaviour
{

    public void PlayDoorKnockSound()
    {
        AudioManager.Instance.PlayDoorKnockSound();
    }

    public void PlayDoorOpenSound()
    {
        AudioManager.Instance.PlayDoorOpenSound();
    }
}
