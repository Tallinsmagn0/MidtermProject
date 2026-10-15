using System;
using UnityEngine;

public class KeyAndHoleLoader : MonoBehaviour, ILoader
{
    [SerializeField] private GameObject keyBallPrefab;
    [SerializeField] private GameObject keyHolePrefab;
    [SerializeField] private GameObject hourglassPrefab;
    [SerializeField] private Transform ballStart;
    [SerializeField] private Transform holeStart;
    [SerializeField] private Transform hourglassStart;
    [SerializeField] private KeyAndHoleTimer timer;

    private GameObject keyBallObject;
    private GameObject keyHoleObject;
    private GameObject hourglassObject;

    public void Load()
    {
        keyBallObject = Instantiate(keyBallPrefab, ballStart.position, Quaternion.identity, transform);
        keyHoleObject = Instantiate(keyHolePrefab, holeStart.position, Quaternion.identity, transform);
        hourglassObject = Instantiate(hourglassPrefab, hourglassStart.position, Quaternion.identity, transform);

        SetHourglassResetEvent();
        SetBallKinematic(true);
    }

    private void SetHourglassResetEvent()
    {
        HourglassInteractable interactable = hourglassObject.GetComponent<HourglassInteractable>();

        if (interactable != null)
        {
            interactable.AddResetEventListener(ResetKeyAndHoleGame);
        }
        else
        {
            throw new NullReferenceException("Hourglass object doesn't have an interactable component");
        }
    }

    private void RemoveHourglassResetEvent()
    {
        HourglassInteractable interactable = hourglassObject.GetComponent<HourglassInteractable>();

        if (interactable != null)
        {
            interactable.RemoveResetEventListener(ResetKeyAndHoleGame);
        }
        else
        {
            throw new NullReferenceException("Hourglass object doesn't have an interactable component");
        }
    }

    public void Unload()
    {
        DeactivateObjects();
    }

    public void DeactivateSelf()
    {
        DeactivateObjects();
        timer.DisableTimer();
    }

    private void DeactivateObjects()
    {
        if (keyBallObject != null)
        {
            keyBallObject.SetActive(false);
        }
        if (keyHoleObject != null)
        {
            keyHoleObject.SetActive(false);
        }
        if (hourglassObject != null)
        {
            RemoveHourglassResetEvent();
            hourglassObject.SetActive(false);
        }
    }

    public void ResetKeyAndHoleGame()
    {
        if (keyBallObject != null)
        {
            ResetBallPosition();
            SetBallKinematic(false);
            timer.StartTimer();
        }
        else
        {
            throw new NullReferenceException("No key ball object found.");
        }
    }

    public void ResetBallPosition()
    {
        keyBallObject.transform.position = ballStart.position;
    }

    public void SetBallKinematic(bool isKinematic)
    {
        Rigidbody rb = keyBallObject.GetComponent<Rigidbody>();

        if (rb != null)
        {
            rb.isKinematic = isKinematic;
        }
        else
        {
            throw new NullReferenceException("Key ball does not have a rigidbody.");
        }
    }

    public void StopKeyAndHoleTimer()
    {
        timer.DisableTimer();
    }
}
