using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class SandClickBehavior : MonoBehaviour
{
    [SerializeField] private UnityEvent<Vector3> OnClickAt;
    [SerializeField] private string[] raycastLayers;
    [SerializeField] private string sandLayer;
    [SerializeField] private Animator playerAnimator;
    [SerializeField] private float clickDelay = 0.4f;
    private bool clicked;

    void Update()
    {
        if (clicked)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            
            if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, LayerMask.GetMask(raycastLayers)))
            {
                if (hit.transform.gameObject.layer == LayerMask.NameToLayer(sandLayer))
                {
                    ClickSandAt(hit.point);
                    playerAnimator.SetTrigger("CastSand");
                }
            }
        }
    }

    public void ClickSandAt(Vector3 point)
    {
        StartCoroutine(SandClickRoutine(point));
    }


    private IEnumerator SandClickRoutine(Vector3 clickPoint)
    {
        yield return new WaitForSeconds(clickDelay);
        
        OnClickAt?.Invoke(clickPoint);
    }

    public void SetClicked(bool value)
    { 
        clicked = value;
    }
}
