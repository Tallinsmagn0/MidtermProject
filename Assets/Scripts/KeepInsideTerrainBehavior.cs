using System;
using UnityEngine;

public class PositionClamp : MonoBehaviour
{
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
        clampedPosition.x = Mathf.Clamp(clampedPosition.x, bounds.min.x, bounds.max.x);
        clampedPosition.z = Mathf.Clamp(clampedPosition.z, bounds.min.z, bounds.max.z);

        transform.position = clampedPosition;
    }
}
