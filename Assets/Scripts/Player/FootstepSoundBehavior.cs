using UnityEngine;

public class FootstepSoundBehavior : MonoBehaviour
{
    public void PlayFootstepSound()
    {
        AudioManager.Instance.PlayFootstepSound();
    }
}
