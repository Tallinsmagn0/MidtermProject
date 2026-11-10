using UnityEngine;
using UnityEngine.Events;

public class HourglassInteractable : MonoBehaviour, IInteractable
{

    [SerializeField] private UnityEvent OnResetHourglass;

    [SerializeField] private Animator animator;

    public void Interact(Interactor interactor)
    {
        ResetHourglass();
        AudioManager.Instance.PlayHourglassSound();
    }

    private void ResetHourglass()
    {
        animator.SetTrigger("Reset");
        OnResetHourglass?.Invoke();
    }

    public void AddResetEventListener(UnityAction eventListener)
    {
        OnResetHourglass.AddListener(eventListener);
    }

    public void RemoveResetEventListener(UnityAction eventListener)
    {
        OnResetHourglass.RemoveListener(eventListener);
    }
}
