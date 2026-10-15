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
            CutsceneManager.Instance.SetUpKeyInHoleCutscene(this);
            CutsceneManager.Instance.RunKeyInHoleCutscene();
            LevelManager.Instance.ClearedCurrentKeyAndHole();
        }
    }

    private void SetKeyBallObject(Collider other)
    {
        keyBallObject = other.gameObject;
    }

    public GameObject GetKeyBallObject()
    {
        return keyBallObject;
    }

    public Animator GetAnimator()
    {
        return keyHoleAnimator;
    }

    public Transform GetCameraPivot()
    {
        return keyHoleCameraPivot;
    }

    public Transform GetBallMovePoint()
    {
        return keyBallMovePoint;
    }
}
