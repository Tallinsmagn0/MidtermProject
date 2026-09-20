using UnityEngine;
using UnityEngine.Events;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float movementSpeed;

    new private Rigidbody rigidbody;
    private float horizontalInput;
    private float verticalInput;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }


    protected virtual void Update()
    { 
        Move(); 
    }

    private void Move()
    {
        Vector3 movementVector = Vector3.ClampMagnitude(new Vector3(horizontalInput, 0, verticalInput), 1);
        movementVector = transform.rotation * movementVector * movementSpeed;
        rigidbody.AddForce(movementVector * Time.deltaTime, ForceMode.VelocityChange);
    }

    public void SetHorizontalInput(float value)
    { horizontalInput = value; }

    public void SetVerticalInput(float value)
    { verticalInput = value; }
}
