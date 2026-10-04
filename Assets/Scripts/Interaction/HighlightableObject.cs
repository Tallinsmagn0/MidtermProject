using UnityEngine;
using UnityEngine.Events;

public class HighlightableObject : MonoBehaviour, IHighlightable
{
    public UnityEvent<HighlightableObject> OnHighlightStart;
    public UnityEvent<HighlightableObject> OnHighlightEnd;

    public void StartHighlight(HighlightBehaviour highlighter)
    { 
        Debug.Log("Start highlight");
        OnHighlightStart?.Invoke(this); 
    }

    public void StopHighlight(HighlightBehaviour highlighter)
    { 
        Debug.Log("Stop highlight");
        OnHighlightEnd?.Invoke(this); 
    }
}