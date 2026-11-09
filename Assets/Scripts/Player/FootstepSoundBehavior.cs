using UnityEngine;

public class FootstepPlayer : MonoBehaviour
{
    [SerializeField] private Animator playerAnimator;
    [SerializeField] private string runStateName;
    [SerializeField] private float stepInterval;
    [SerializeField] private float minVelocity;

    private float stepTimer;
    private int runStateHash;

    void Start()
    {
        stepTimer = stepInterval;
        runStateHash = Animator.StringToHash(runStateName);
    }

    void Update()
    {
        if (IsRunning())
        {
            stepTimer -= Time.deltaTime;

            if (stepTimer <= 0f)
            {
                AudioManager.Instance.PlayFootstepSound();
                stepTimer = stepInterval;
            }
        }
        else
        {
            stepTimer = stepInterval;
        }
    }

    private bool IsRunning()
    {
        if (playerAnimator.IsInTransition(0))
        {
            AnimatorStateInfo nextStateInfo = playerAnimator.GetNextAnimatorStateInfo(0);
            
            return nextStateInfo.shortNameHash == runStateHash;
        } 
        else
        {
            return playerAnimator.GetCurrentAnimatorStateInfo(0).IsName(runStateName);
        }
    }
}
