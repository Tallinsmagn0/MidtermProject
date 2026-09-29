using UnityEngine;
using UnityEngine.Playables; 

public class TimelineController : MonoBehaviour
{
    [SerializeField] private PlayableDirector timelineDirector;

    public void StartTimeline()
    {
        if (timelineDirector != null)
        {
            timelineDirector.Play();
        }
    }
}
