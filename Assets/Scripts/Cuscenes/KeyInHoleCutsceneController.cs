using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;
using Unity.Cinemachine;

public class KeyInHoleCutsceneController : MonoBehaviour
{
    // Timeline and Cinemachine references
    [SerializeField] private PlayableDirector director;
    [SerializeField] private CinemachineCamera mainCamera;

    // Door animation
    [SerializeField] private Animator doorOpenAnimator;
    [SerializeField] private CinemachineCamera doorCamera;
    [SerializeField] private string doorTrackName;

    // KeyBall animation
    [SerializeField] private string keyBallTrackName;
    private Animator keyBallAnimator;

    // KeyHole animation
    [SerializeField] private CinemachineCamera keyHoleCamera;

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
    }

    private void UpdateAnimatorState()
    {
        doorOpenAnimator.Play("Opened", 0, 0f);
        doorOpenAnimator.SetBool("Open", true);
    }
}
