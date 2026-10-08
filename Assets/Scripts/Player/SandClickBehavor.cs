using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class SandClickBehavior : MonoBehaviour
{
    [SerializeField] private UnityEvent<Vector3> OnClickAt;
    [SerializeField] private string layerMask;
    [SerializeField] private Animator playerAnimator;
    [SerializeField] private float clickDelay = 0.4f;
    private bool clicked;

    void Update()
    {
        if (clicked)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            
            if (Physics.Raycast(ray, out RaycastHit hit, LayerMask.GetMask(layerMask)))
            {
                StartCoroutine(SandClickRoutine(hit.point));
                playerAnimator.SetTrigger("CastSand");
            }
        }
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
