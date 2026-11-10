using UnityEngine;

public class KeyInHoleSoundBehavior : MonoBehaviour
{
    public void PlayBallShrinkSound()
    {
        AudioManager.Instance.PlayKeyBallShrinkSound();
    }

    public void PlayHoleSpinSound()
    {
        AudioManager.Instance.PlayKeyHoleSpinSound();
    }

    public void PlayHoleShrinkSound()
    {
        AudioManager.Instance.PlayKeyHoleShrinkSound();
    }
}
