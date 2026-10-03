using UnityEngine;
using UnityEngine.Events;

public class ScreenClickBehavior : MonoBehaviour
{
    [SerializeField] private UnityEvent<Vector3> OnClickAt;
    [SerializeField] private string layerMask;
    private bool clicked;

    void Update()
    {
        if (clicked)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            
            if (Physics.Raycast(ray, out RaycastHit hit, LayerMask.GetMask(layerMask)))
            {
                OnClickAt?.Invoke(hit.point);
            }
        }
    }

    public void SetClicked(bool value)
    { 
        clicked = value;
    }
}
