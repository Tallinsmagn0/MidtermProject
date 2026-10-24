using UnityEngine;
using UnityEngine.Events;

public class PlayerMovementBehavior : MonoBehaviour
{
    [SerializeField] private float movementSpeed;
    [SerializeField] private Animator playerAniamtor;

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
        playerAniamtor.SetFloat("Movement", movementVector.magnitude);
    }

    public void SetHorizontalInput(float value)
    { horizontalInput = value; }

    public void SetVerticalInput(float value)
    { verticalInput = value; }
}
