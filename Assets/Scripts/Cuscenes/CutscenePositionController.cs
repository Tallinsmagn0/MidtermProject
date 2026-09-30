using System.Collections;
using UnityEngine;

public class CutscenePositionController : MonoBehaviour
{
    
    public void MoveToPoint()
    {
        Transform target = KeyInHoleCutsceneController.Instance.GetKeyBallMovePoint();
        float duration = KeyInHoleCutsceneController.Instance.GetKeyBallMoveDuration();
        StartCoroutine(MoveToPosition(target, duration));
    }

    IEnumerator MoveToPosition(Transform target, float duration)
    {
        float timeElapsed = 0f;
        Transform startTransform = transform;

        while (timeElapsed < duration)
        {
            // Calculate the interpolation factor 't' between 0 and 1
            float t = timeElapsed / duration;

            // Interpolate Position
            transform.position = Vector3.Lerp(startTransform.position, target.position, t);

            // Interpolate Rotation (Slerp is best for Quaternions)
            transform.rotation = Quaternion.Slerp(startTransform.rotation, target.rotation, t);

            timeElapsed += Time.deltaTime;
            yield return null; // Wait for the next frame
        }

        // Snap to final destination exactly when finished
        transform.position = target.position;
        transform.rotation = target.rotation;
    }
}
