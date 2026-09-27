using UnityEngine;

public class LevelTrigger : MonoBehaviour
{

    [SerializeField] private Animator doorAnimator;

    private void OnTriggerEnter(Collider other)
    {
        if (other.attachedRigidbody != null && other.attachedRigidbody.CompareTag("Player"))
        {
            LevelManager.Instance.IncrementLevel();
            doorAnimator.SetBool("Open", false);
            gameObject.SetActive(false);
        }
    }
}
