using System;
using UnityEngine;

public class KeyInHoleCutsceneTrigger : MonoBehaviour
{
    [SerializeField] private Animator keyHoleAnimator;
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
        KeyInHoleCutsceneController.Instance.SetUpCameras(keyHoleCameraPivot);
        KeyInHoleCutsceneController.Instance.SetUpDirector(keyBallObject, keyBallMovePoint, keyHoleAnimator);
    }

    private void SetKeyBallObject(Collider other)
    {
        keyBallObject = other.gameObject;
    }
}
