using UnityEngine;
using UnityEngine.Events;

public class HourglassInteractable : MonoBehaviour, IInteractable
{

    [SerializeField] private UnityEvent OnResetHourglass;

    public void Interact(Interactor interactor)
    {
        ResetHourglass();
    }

    private void ResetHourglass()
    {
        OnResetHourglass?.Invoke();
    }
}
