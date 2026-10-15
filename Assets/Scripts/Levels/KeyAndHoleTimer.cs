using UnityEngine;
using UnityEngine.Events;

public class KeyAndHoleTimer : MonoBehaviour
{
    [SerializeField] private float maxTime;
    [SerializeField] private UnityEvent<float> OnAdvanceTimer;
    [SerializeField] private UnityEvent<bool> OnTimerEnabled;

    private bool isTicking = false;
    private float currentTime;

    
    void Update()
    {
        RunTimer();
    }

    private void RunTimer()
    {
        if (isTicking)
        {
            if (currentTime > 0f)
            {
                currentTime -= Time.deltaTime;
                OnAdvanceTimer?.Invoke(currentTime);
            }
            else
            {
                Debug.Log("FINISHED");
                GameManager.Instance.GameOver();
            }
        }
    }

    public void StartTimer()
    {
        isTicking = true;
        currentTime = maxTime;
        OnTimerEnabled?.Invoke(true);
    }

    public void DisableTimer()
    {
        isTicking = false;
        OnTimerEnabled?.Invoke(false);
    }

}
