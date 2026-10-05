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

    private GameObject keyBallObject;
    private GameObject keyHoleObject;
    private GameObject hourglassObject;

    public void Load()
    {
        keyBallObject = Instantiate(keyBallPrefab, ballStart.position, Quaternion.identity, transform);
        keyHoleObject = Instantiate(keyHolePrefab, holeStart.position, Quaternion.identity, transform);
        hourglassObject = Instantiate(hourglassPrefab, hourglassStart.position, Quaternion.identity, transform);

        SetHourglassResetEvent();
    }

    private void SetHourglassResetEvent()
    {
        HourglassInteractable interactable = hourglassObject.GetComponent<HourglassInteractable>();

        if (interactable != null)
        {
            interactable.AddResetEventListener(ResetBallPosition);
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
            interactable.RemoveResetEventListener(ResetBallPosition);
        }
        else
        {
            throw new NullReferenceException("Hourglass object doesn't have an interactable component");
        }
    }

    public void Unload()
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

    public void ResetBallPosition()
    {
        if (keyBallObject != null)
        {
            keyBallObject.transform.position = ballStart.position;
        }
    }
}
