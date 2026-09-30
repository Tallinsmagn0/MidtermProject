using UnityEngine;

public class KeyInHoleCutsceneTrigger : MonoBehaviour
{
    [SerializeField] private Transform doorCameraPivot;
    [SerializeField] private Transform keyHoleCameraPivot;

    [SerializeField] private Animator doorOpenAnimator;
    [SerializeField] private Transform keyBallMovePoint;
    private GameObject keyBallObject;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("KeyBall"))
        {
            SetKeyBallObject(other);
            SetUpCutscene();
            KeyInHoleCutsceneController.Instance.PlayCutscene();
        }
    }

    private void SetUpCutscene()
    {
        KeyInHoleCutsceneController.Instance.SetUpCameras(keyHoleCameraPivot, doorCameraPivot);
        KeyInHoleCutsceneController.Instance.SetUpDirector(keyBallObject, keyBallMovePoint, doorOpenAnimator);
    }

    private void SetKeyBallObject(Collider other)
    {
        keyBallObject = other.gameObject;
    }
}
