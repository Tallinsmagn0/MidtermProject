using UnityEngine;

public class KeyInHoleCutsceneTrigger : MonoBehaviour
{
    [SerializeField] private Transform doorCameraPivot;
    [SerializeField] private Transform keyHoleCameraPivot;

    [SerializeField] private Animator doorOpenAnimator;
    private Animator keyBallAnimator;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("KeyBall"))
        {
            SetKeyBallAniamtor(other);

            SetUpCutscene(other);
            KeyInHoleCutsceneController.Instance.PlayCutscene();
        }
    }

    private void SetUpCutscene(Collider other)
    {
        SetKeyBallAniamtor(other);
        KeyInHoleCutsceneController.Instance.SetUpCameras(keyHoleCameraPivot, doorCameraPivot);
        KeyInHoleCutsceneController.Instance.SetUpDirector(keyBallAnimator, doorOpenAnimator);
    }

    private void SetKeyBallAniamtor(Collider other)
    {
        keyBallAnimator = other.gameObject.GetComponent<Animator>();
    }
}
