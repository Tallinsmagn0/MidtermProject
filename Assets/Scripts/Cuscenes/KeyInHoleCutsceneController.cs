using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;
using Unity.Cinemachine;

public class KeyInHoleCutsceneController : MonoBehaviour
{
    // Timeline and Cinemachine references
    [SerializeField] private PlayableDirector director;
    [SerializeField] private CinemachineCamera doorCamera;
    [SerializeField] private CinemachineCamera keyHoleCamera;

    // Camera pivots
    [SerializeField] private Transform virtualCameraContainer;
    [SerializeField] private Transform doorCameraPivot;
    [SerializeField] private Transform keyHoleCameraPivot;

    // Track names
    [SerializeField] private string doorTrackName;
    [SerializeField] private string keyBallTrackName;

    // Animators
    [SerializeField] private Animator doorOpenAnimator;
    private Animator keyBallAnimator;

    // KeyHole animation

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("KeyBall"))
        {
            SetUpCutscene(other);
            PlayCutscene();
        }
    }

    private void PlayCutscene()
    {
        director.Play();
    }

    private void SetUpCutscene(Collider other)
    {
        GetKeyBallAniamtor(other);
        SetUpCameras();
        SetUpDirector();
    }

    private void GetKeyBallAniamtor(Collider other)
    {
        keyBallAnimator = other.gameObject.GetComponent<Animator>();
    }

    private void SetUpCameras()
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

    private void SetUpDirector()
    {
        director.stopped += OnTimelineStopped;
        SetAnimationTracks();
    }

    private void SetAnimationTracks()
    {
        TimelineAsset timelineAsset = director.playableAsset as TimelineAsset;

        foreach (TrackAsset track in timelineAsset.GetOutputTracks())
        {
            if (track is AnimationTrack && track.name == keyBallTrackName)
            {
                director.SetGenericBinding(track, keyBallAnimator);
            }
            else if (track is AnimationTrack && track.name == doorTrackName)
            {
                director.SetGenericBinding(track, doorOpenAnimator);
            }
        }
    }

    private void OnTimelineStopped(PlayableDirector _director)
    {
        if (director == _director)
        {
            DisableCameras();
            UpdateAnimatorState();
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
}
