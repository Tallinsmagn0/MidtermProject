using System;
using UnityEngine;

public class KeepInsideTerrainBehavior : MonoBehaviour
{
    [SerializeField] private Transform modelTransform;

    private float radius;

    void Start()
    {
        radius = modelTransform.localScale.x / 2;
    }

    void LateUpdate()
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
        Vector3 clampedPosition = transform.position;
        clampedPosition.x = Mathf.Clamp(clampedPosition.x, bounds.min.x + radius, bounds.max.x - radius);
        clampedPosition.z = Mathf.Clamp(clampedPosition.z, bounds.min.z + radius, bounds.max.z - radius);

        transform.position = clampedPosition;
    }
}
