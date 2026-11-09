using System;
using UnityEngine;

public class KeepInsideTerrainBehavior : MonoBehaviour
{
    [SerializeField] private Transform modelTransform;

    private Rigidbody rb;
    private float radius;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        radius = modelTransform.localScale.x / 2;
    }

    void FixedUpdate()
    {
        TryPositionClamp();
    }

    private void TryPositionClamp()
    {
        Terrain currentTerrain = LevelManager.Instance.GetCurrentTerrain();

        if (currentTerrain != null)
        {
            TerrainCollider collider = currentTerrain.GetComponent<TerrainCollider>();

            if (collider != null)
            {
                ClampPosition(collider.bounds);
            }
            else
            {
                throw new NullReferenceException("Can't clamp position because terrain collider is null");
            }
        }
        else
        {
            throw new NullReferenceException("Can't clamp position because terrain is null");
        }
    }

    private void ClampPosition(Bounds bounds)
    {
        Vector3 clampedPosition = rb.position;
        float minX = bounds.min.x + radius;
        float maxX = bounds.max.x - radius;
        float minZ = bounds.min.z + radius;
        float maxZ = bounds.max.z - radius;

        clampedPosition.x = Mathf.Clamp(clampedPosition.x, minX, maxX);
        clampedPosition.z = Mathf.Clamp(clampedPosition.z, minZ, maxZ);

        rb.position = clampedPosition;
        
        if (clampedPosition.x == minX || clampedPosition.x == maxX)
        {
            rb.linearVelocity = new Vector3(0, rb.linearVelocity.y, rb.linearVelocity.z);
        }
        if (clampedPosition.z == minZ || clampedPosition.z == maxZ)
        {
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, rb.linearVelocity.y, 0);
        }
    }
}
