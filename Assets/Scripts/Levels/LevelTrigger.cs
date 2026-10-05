using UnityEngine;

public class LevelTrigger : MonoBehaviour
{

    [SerializeField] private Animator entranceDoorAnimator;
    [SerializeField] private Animator exitDoorAnimator;

    private void OnTriggerEnter(Collider other)
    {
        if (other.attachedRigidbody != null && other.attachedRigidbody.CompareTag("Player"))
        {
            LevelManager.Instance.IncrementLevel();
            entranceDoorAnimator.SetBool("Open", false);
            exitDoorAnimator.SetBool("Open", true);
            gameObject.SetActive(false);
        }
    }
}
