using System.Collections;
using UnityEngine;

public class KeyBallCutscenePositionController : MonoBehaviour
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
            float t = timeElapsed / duration;

            transform.position = Vector3.Lerp(startTransform.position, target.position, t);
            transform.rotation = Quaternion.Slerp(startTransform.rotation, target.rotation, t);

            timeElapsed += Time.deltaTime;

            yield return null;
        }
        transform.position = target.position;
        transform.rotation = target.rotation;
    }
}
