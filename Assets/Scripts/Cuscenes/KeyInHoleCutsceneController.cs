using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;
using Unity.Cinemachine;
using System;

public class KeyInHoleCutsceneController : Singleton<KeyInHoleCutsceneController>
{
    // Timeline references
    [SerializeField] private PlayableDirector director;

    // Camera references
    [SerializeField] private CinemachineCamera keyHoleCamera;
    [SerializeField] private CinemachineCamera doorCamera;
    [SerializeField] private Transform virtualCameraContainer;

    // Animation Track names
    [SerializeField] private string keyBallMoveSignalTrackName;
    [SerializeField] private string keyBallAnimationTrackName;
    [SerializeField] private string doorOpenAnimationTrackName;

    [SerializeField] private float keyBallMoveDuration;

    // Object references references
    private Animator keyBallAnimator;
    private SignalReceiver keyBallMoveSignalReciever;
    private Transform keyBallMovePoint;
    private Animator doorOpenAnimator;

    public void PlayCutscene()
    {
        director.Play();
    }

    public void SetUpCameras(Transform keyHoleCameraPivot, Transform doorCameraPivot)
    {
        keyHoleCamera.enabled = true;
        doorCamera.enabled = true;

        keyHoleCamera.gameObject.transform.SetParent(keyHoleCameraPivot);
        doorCamera.gameObject.transform.SetParent(doorCameraPivot);

        keyHoleCamera.gameObject.transform.localPosition = Vector3.zero;
        keyHoleCamera.gameObject.transform.localRotation = Quaternion.identity;
        doorCamera.gameObject.transform.localPosition = Vector3.zero;
        doorCamera.gameObject.transform.localRotation = Quaternion.identity;
    }

    public void SetUpDirector(GameObject _keyBallObject, Transform _keyBallMovePoint, Animator _doorOpenAnimator)
    {
        SetObjectReferences(_keyBallObject, _keyBallMovePoint, _doorOpenAnimator);
        SetTracks();
        director.stopped += OnTimelineStopped;
    }

    private void SetObjectReferences(GameObject _keyBallObject, Transform _keyBallMovePoint, Animator _doorOpenAnimator)
    {
        keyBallAnimator = _keyBallObject.GetComponent<Animator>();
        keyBallMoveSignalReciever = _keyBallObject.GetComponent<SignalReceiver>();
        keyBallMovePoint = _keyBallMovePoint;
        doorOpenAnimator = _doorOpenAnimator;

        Rigidbody keyBallRb = _keyBallObject.GetComponent<Rigidbody>();
        keyBallRb.isKinematic = true;
    }

    private void SetTracks()
    {
        TimelineAsset timelineAsset = director.playableAsset as TimelineAsset;

        foreach (TrackAsset track in timelineAsset.GetOutputTracks())
        {
            if (track is AnimationTrack && track.name == keyBallAnimationTrackName)
            {
                if (keyBallAnimator != null)
                {
                    director.SetGenericBinding(track, keyBallAnimator);
                }
                else
                {
                    throw new NullReferenceException("Key ball aniamtor reference was null");
                }
            }
            else if (track is AnimationTrack && track.name == doorOpenAnimationTrackName)
            {
                if (doorOpenAnimator != null)
                {
                    director.SetGenericBinding(track, doorOpenAnimator);
                }
                else
                {
                    throw new NullReferenceException("Door open aniamtor reference was null");
                }
            }
            else if (track is SignalTrack && track.name == keyBallMoveSignalTrackName)
            {
                director.SetGenericBinding(track, keyBallMoveSignalReciever);
            }
        }
    }

    private void OnTimelineStopped(PlayableDirector _director)
    {
        if (director == _director)
        {
            DisableCameras();
            UpdateAnimatorState();
            UnsetObjectReferences();
            director.stopped -= OnTimelineStopped;
        }
    }

    private void DisableCameras()
    {
        DisableCamera(keyHoleCamera);
        DisableCamera(doorCamera);
    }

    private void DisableCamera(CinemachineCamera camera)
    {
        if (camera != null)
        {
            camera.enabled = false;
            camera.gameObject.transform.SetParent(virtualCameraContainer);
        }
    }

    private void UpdateAnimatorState()
    {
        if (doorOpenAnimator != null)
        {
            doorOpenAnimator.Play("Opened", 0, 0f);
            doorOpenAnimator.SetBool("Open", true);
        }
    }

    private void UnsetObjectReferences()
    {
        keyBallAnimator = null;
        keyBallMoveSignalReciever = null;
        keyBallMovePoint = null;
        doorOpenAnimator = null;
    }

    public Transform GetKeyBallMovePoint()
    {
        return keyBallMovePoint;
    }

    public float GetKeyBallMoveDuration()
    {
        return keyBallMoveDuration;
    }
}
