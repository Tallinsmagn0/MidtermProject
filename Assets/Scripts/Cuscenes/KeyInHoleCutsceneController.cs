using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;
using Unity.Cinemachine;

public class KeyInHoleCutsceneController : MonoBehaviour
{

    [SerializeField] private PlayableDirector director;
    [SerializeField] private CinemachineBrain brain;
    [SerializeField] private string trackName;

    private Animator keyBallAnimator;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("KeyBall"))
        {
            SetAnimators(other);
            PlayCutscene();
        }
    }

    private void PlayCutscene()
    {
        brain.enabled = true;

        SetUpDirector();
        director.Play();
    }

    private void SetAnimators(Collider other)
    {
        keyBallAnimator = other.gameObject.GetComponent<Animator>();
    }

    private void SetUpDirector()
    {
        TimelineAsset timelineAsset = director.playableAsset as TimelineAsset;

        foreach (TrackAsset track in timelineAsset.GetOutputTracks())
        {
            if (track is AnimationTrack && track.name == trackName)
            {
                director.SetGenericBinding(track, keyBallAnimator);
                break;
            }
        }
    }
}
