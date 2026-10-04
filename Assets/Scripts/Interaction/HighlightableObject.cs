using UnityEngine;
using UnityEngine.Events;

public class HighlightableObject : MonoBehaviour, IHighlightable
{
    public UnityEvent<HighlightableObject> OnHighlightStart;
    public UnityEvent<HighlightableObject> OnHighlightEnd;

    public void StartHighlight(HighlightBehaviour highlighter)
    {
        OnHighlightStart?.Invoke(this); 
    }

    public void StopHighlight(HighlightBehaviour highlighter)
    {
        OnHighlightEnd?.Invoke(this); 
    }
}