using UnityEngine;

public class Interactor : MonoBehaviour
{
    [SerializeField] private string layerMask;
    [SerializeField] private float interactionDistance;
    protected RaycastHit info;
    protected bool interactInput;

    protected virtual void Update()
    {
        if (interactInput && HitObject())
        {
            Interact();
        }
    }

    protected virtual void Interact()
    {
        IInteractable target = info.transform.GetComponent<IInteractable>();

        if (target != null)
        {
            target.Interact(this);
        }
    }

    private bool HitObject()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        return Physics.Raycast(ray, out info, interactionDistance, LayerMask.GetMask(layerMask));
    }

    public void SetInteractInput(bool value)
    { 
        interactInput = value;
    }
}
