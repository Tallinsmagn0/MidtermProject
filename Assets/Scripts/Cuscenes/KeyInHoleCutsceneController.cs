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
    [SerializeField] private string keyBallTrackName;
    [SerializeField] private string doorTrackName;

// Animation Track references
    private Animator keyBallAnimator;
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

    public void SetUpDirector(Animator _keyBallAnimator, Animator _doorOpenAnimator)
    {
        keyBallAnimator = _keyBallAnimator;
        doorOpenAnimator = _doorOpenAnimator;
        
        SetAnimationTracks();
        director.stopped += OnTimelineStopped;
    }

    private void SetAnimationTracks()
    {
        TimelineAsset timelineAsset = director.playableAsset as TimelineAsset;

        foreach (TrackAsset track in timelineAsset.GetOutputTracks())
        {
            if (track is AnimationTrack && track.name == keyBallTrackName)
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
            else if (track is AnimationTrack && track.name == doorTrackName)
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
        }
    }

    private void OnTimelineStopped(PlayableDirector _director)
    {
        if (director == _director)
        {
            DisableCameras();
            UpdateAnimatorState();
            UnsetAnimatorReferences();
            director.stopped -= OnTimelineStopped;
        }
    }

    private void DisableCameras()
    {
        keyHoleCamera.enabled = false;
        doorCamera.enabled = false;

        keyHoleCamera.gameObject.transform.SetParent(virtualCameraContainer);
        keyHoleCamera.gameObject.transform.SetParent(virtualCameraContainer);
    }

    private void UpdateAnimatorState()
    {
        doorOpenAnimator.Play("Opened", 0, 0f);
        doorOpenAnimator.SetBool("Open", true);
    }

    private void UnsetAnimatorReferences()
    {
        doorOpenAnimator = null;
        keyBallAnimator = null;
    }
}
