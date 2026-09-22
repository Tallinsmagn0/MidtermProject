using UnityEngine;
using UnityEngine.Events;

public class ScreenClickBehavior : MonoBehaviour
{
    [SerializeField] private UnityEvent<Vector3> OnClickAt;
    private bool clicked;

    void Update()
    {
        if (clicked)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                OnClickAt?.Invoke(hit.point);
            }
        }
    }

    public void SetClicked(bool value)
    { 
        this.clicked = value;
    }
}
