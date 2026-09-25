using UnityEngine;

public class LevelTrigger : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (other.attachedRigidbody != null && other.attachedRigidbody.CompareTag("Player"))
        {
            LevelManager.Instance.IncrementLevel();
            gameObject.SetActive(false);
        }
    }
}
