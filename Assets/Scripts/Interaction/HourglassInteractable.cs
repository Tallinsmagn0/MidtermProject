using UnityEngine;
using UnityEngine.Events;

public class HourglassInteractable : MonoBehaviour, IInteractable
{

    [SerializeField] private UnityEvent OnResetHourglass;

    [SerializeField] private Animator animator;

    public void Interact(Interactor interactor)
    {
        ResetHourglass();
    }

    private void ResetHourglass()
    {
        animator.SetTrigger("Reset");
        OnResetHourglass?.Invoke();
    }
}
