using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class TerrainExclusionController : MonoBehaviour
{
    [SerializeField] private string exclusionsTag;

    public List<Collider> GetExclusionColliders()
    {
        return GetComponentsInChildren<Collider>(true)
        .Where(t => t.CompareTag(exclusionsTag))
        .ToList();
    }

    public void SetExclusionCollider(bool enable)
    {
        List<Collider> exclusionColliders = GetExclusionColliders();

        foreach (Collider exclusionCollider in exclusionColliders)
        {
            exclusionCollider.gameObject.SetActive(enable);
        }
    }
}
