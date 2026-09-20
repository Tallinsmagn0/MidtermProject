using System;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class PlayerInputAxis
{
    [SerializeField]
    private string axisName;

    [SerializeField]
    private UnityEvent<float> axisAction;

    private float value;

    public string GetName()
    {  return axisName; }

    public float GetValue()
    {
        value = Input.GetAxis(axisName);
        return value;
    }

    public void FireAction()
    {
        axisAction?.Invoke(value);
    }
}

[Serializable]
public class PlayerInputButton
{
    [SerializeField]
    private string buttonName;

    [SerializeField]
    private UnityEvent<bool> buttonAction;

    private bool value;

    public string GetName()
    { return buttonName; }

    public bool GetValue()
    {
        value = Input.GetButtonDown(buttonName);
        return value;
    }

    public void FireAction()
    {
        buttonAction?.Invoke(value);
    }
}

public class PlayerInput : MonoBehaviour
{
    [Header("Input Variables")]
    public PlayerInputAxis[] inputAxes;
    public PlayerInputButton[] inputButtons;

    // Update is called once per frame
    void Update()
    {
        foreach (var axis in inputAxes)
        {
            axis.GetValue();
            axis.FireAction();
        }

        foreach (var button in inputButtons)
        {
            button.GetValue();
            button.FireAction();
        }
    }
}
