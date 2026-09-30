using System;
using UnityEngine;

public class KeyInHoleCutsceneTrigger : MonoBehaviour
{
    [SerializeField] private Transform keyHoleCameraPivot;
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
        Transform doorCameraPivot = LevelManager.Instance.GetCUrrentDoorCameraPivot();
        Animator doorOpenAnimator = LevelManager.Instance.GetCurrentDoorAnimator();

        if (doorCameraPivot != null && doorOpenAnimator != null)
        {
            KeyInHoleCutsceneController.Instance.SetUpCameras(keyHoleCameraPivot, doorCameraPivot);
        KeyInHoleCutsceneController.Instance.SetUpDirector(keyBallObject, keyBallMovePoint, doorOpenAnimator);
        }
        else
        {
            throw new NullReferenceException("Couldn't set up cutscene because door references were null");
        }
    }

    private void SetKeyBallObject(Collider other)
    {
        keyBallObject = other.gameObject;
    }
}
