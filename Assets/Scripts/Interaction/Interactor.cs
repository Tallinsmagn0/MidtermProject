using UnityEngine;

public class Interactor : MonoBehaviour
{
    [SerializeField] private string layerMask;
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private float interactionDistance;

    protected RaycastHit info;
    protected bool interactInput;

    protected virtual void Update()
    {
        if (HitObject())
            Interact();
    }

    protected virtual void Interact()
    {
        if(interactInput)
        {
            IInteractable target = info.transform.GetComponent<IInteractable>();
            
            if (target != null)
            {
                target.Interact(this);
            }
        }
    }

    public bool HitObject()
    {
        Ray interactionRay = new Ray(cameraTransform.position, cameraTransform.forward);
        bool result = Physics.Raycast(interactionRay, out info, interactionDistance, LayerMask.GetMask(layerMask));

        Vector3 target = result ? info.point : interactionRay.origin + interactionRay.direction * 5;
        Debug.DrawLine(interactionRay.origin, target, Color.red);
        return result;
    }

    public void SetInteractInput(bool value)
    { 
        interactInput = value;
    }
}
