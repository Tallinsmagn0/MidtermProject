using UnityEngine;
using UnityEngine.Events;

public class ScreenClickBehavior : MonoBehaviour
{
    [SerializeField] private UnityEvent<Vector3> OnClickAt;
    [SerializeField] private string colliderTag;
    private bool clicked;

    void Update()
    {
        if (clicked)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                if (hit.collider.CompareTag(colliderTag))
                {
                    OnClickAt?.Invoke(hit.point);
                }
            }
        }
    }

    public void SetClicked(bool value)
    { 
        this.clicked = value;
    }
}
