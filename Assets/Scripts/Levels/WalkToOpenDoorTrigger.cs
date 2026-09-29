using UnityEngine;

public class WalkToOpenDoorTrigger : MonoBehaviour
{

    [SerializeField] private Animator doorAnimator;

    private void OnTriggerEnter(Collider other)
    {
        if (other.attachedRigidbody != null && other.attachedRigidbody.CompareTag("Player"))
        {
            doorAnimator.SetBool("Open", true);
            gameObject.SetActive(false);
        }
    }
}
