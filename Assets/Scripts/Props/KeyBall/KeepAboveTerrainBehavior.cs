using UnityEngine;

public class KeepAboveTerrainBehavior : MonoBehaviour
{
    [SerializeField] private string terrainTag;

    [SerializeField] private float addedHeight;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (rb.IsSleeping())
        {
            rb.WakeUp();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag(terrainTag))
        {
            Terrain terrain = collision.gameObject.GetComponent<Terrain>();
            float terrainHeight = terrain.GetPosition().y + terrain.SampleHeight(transform.position);
            float finalHeight = Mathf.Max(terrainHeight + addedHeight, transform.position.y);

            Vector3 targetPosition = new Vector3(transform.position.x, finalHeight, transform.position.z);
            transform.position = targetPosition;
        }
    }
}
