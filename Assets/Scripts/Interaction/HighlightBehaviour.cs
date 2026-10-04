using UnityEngine;

public class HighlightBehaviour : Interactor
{
    IHighlightable target;

    protected override void Update()
    {
        HitObject();
        Interact();
    }

    protected override void Interact()
    {
        IHighlightable newTarget = null;

        if (info.transform != null)
        {
            newTarget = info.transform.GetComponent<IHighlightable>();
        }
        if (newTarget != null)
        {
            if (newTarget != target)
            {
                target?.StopHighlight(this);
                target = newTarget;
                target.StartHighlight(this);
            }
        }
        else
        {
            target?.StopHighlight(this);
            target = null;
        }
    }
}
