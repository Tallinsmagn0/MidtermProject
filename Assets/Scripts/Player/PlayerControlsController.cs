using UnityEngine;

public class PlayerControlsController : MonoBehaviour
{
    [SerializeField] PlayerInput input;
    [SerializeField] PlayerMovementBehavior movementBehaviour;
    [SerializeField] PlayerJumpBehaviour jumpBehaviour;
    [SerializeField] PlayerTurnXBehavior turnXBehaviour;
    [SerializeField] PlayerTurnYBehavior turnYBehaviour;
    [SerializeField] BrushSizeScrollBehavior brushController;
    [SerializeField] Animator animator;

    public void SetControlsEnbaled(bool isEnabled)
    {
        input.enabled = isEnabled;
        movementBehaviour.enabled = isEnabled;
        jumpBehaviour.enabled = isEnabled;
        turnXBehaviour.enabled = isEnabled;
        turnYBehaviour.enabled = isEnabled;
        brushController.enabled = isEnabled;
        
        animator.SetFloat("Movement", 0f);
    }
}
