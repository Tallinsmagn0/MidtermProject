using UnityEngine;

public class KeyInHoleCutsceneController : MonoBehaviour
{

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("KeyBall"))
        {
            
        }
    }
}
