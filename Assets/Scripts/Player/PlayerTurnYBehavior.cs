using UnityEngine;

public class PlayerTurnYBehavior : MonoBehaviour
{
    [SerializeField] private Transform cameraPivot;
    [SerializeField] private CursorLockMode cursorLockMode = CursorLockMode.Locked;

    [SerializeField] private float verticalSpeed;
    [SerializeField] private float maximumAngle;
    [SerializeField] private float minimumAngle;
    private float verticalInput;

    private float verticalRotation;

    private void Start()
    { 
        Cursor.lockState = cursorLockMode; 
    }

    private void Update()
    {
        UpdateRotation();
        TurnCamera();
    }

    void UpdateRotation()
    {
        verticalRotation += verticalInput * verticalSpeed * Time.deltaTime;
        verticalRotation = Mathf.Clamp(verticalRotation, minimumAngle, maximumAngle);
    }

    void TurnCamera()
    { 
        cameraPivot.localRotation = Quaternion.AngleAxis(verticalRotation, Vector3.right); 
    }

    public void SetCameraVerticalInput(float value)
    { 
        verticalInput = value; 
    }
}
