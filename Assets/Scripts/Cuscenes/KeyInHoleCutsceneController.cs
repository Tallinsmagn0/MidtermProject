using UnityEngine;
using UnityEngine.Playables;
using Unity.Cinemachine;

public class KeyInHoleCutsceneController : MonoBehaviour
{

    [SerializeField] private PlayableDirector director;
    [SerializeField] private CinemachineBrain brain;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("KeyBall"))
        {
            brain.enabled = true;
            director.Play();
        }
    }
}
