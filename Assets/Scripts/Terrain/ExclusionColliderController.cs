using UnityEngine;

public class ExclusionColliderController : MonoBehaviour
{
    [SerializeField] private Terrain terrain;
    [SerializeField] BoxCollider rightCollider;
    [SerializeField] BoxCollider bottomCollider;
    [SerializeField] BoxCollider leftCollider;
    [SerializeField] BoxCollider topCollider;
    [SerializeField] float width;

    public void SetBorderColliders()
    {
        TerrainData data = terrain.terrainData;

        float centerX = data.size.x / 2;
        float centerZ = data.size.z / 2;

        if (rightCollider != null)
        {
            rightCollider.transform.localPosition = new Vector3(centerX, 0, 0);
            rightCollider.size = new Vector3(data.size.x + 2 * width, width, width);
        }
        if (bottomCollider != null)
        {
            bottomCollider.transform.localPosition = new Vector3(0, 0, centerZ);
            bottomCollider.size = new Vector3(data.size.z + 2 * width, width, width);
        }
        if (leftCollider != null)
        {
            leftCollider.transform.localPosition = new Vector3(centerX, 0, data.size.z);
            leftCollider.size = new Vector3(data.size.x + 2 * width, width, width);
        }
        if (topCollider != null)
        {
            topCollider.transform.localPosition = new Vector3(data.size.x, 0, centerZ);
            topCollider.size = new Vector3(data.size.z + 2 * width, width, width);
        }
    }
}
