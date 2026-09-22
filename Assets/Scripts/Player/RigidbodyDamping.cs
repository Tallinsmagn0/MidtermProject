using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class RigidbodyDamping : MonoBehaviour
{
    private Rigidbody rb;
    
    [SerializeField] private float horizontalDamping = 2.0f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.linearDamping = 0f; 
    }

    void FixedUpdate()
    {
        Vector3 velocity = rb.linearVelocity;

        float dampingFactor = 1f - horizontalDamping * Time.fixedDeltaTime;
        dampingFactor = Mathf.Clamp01(dampingFactor);

        velocity.x *= dampingFactor;
        velocity.z *= dampingFactor;

        rb.linearVelocity = velocity;
    }
}
