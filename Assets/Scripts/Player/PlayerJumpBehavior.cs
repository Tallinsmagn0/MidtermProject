using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerJumpBehaviour : MonoBehaviour
{
    [SerializeField] protected Transform footPivot;
    [SerializeField] protected float force;
    [SerializeField] protected float groundedDistance;
    new private Rigidbody rigidbody;
    private bool jumpInput;

    private void Start()
    { 
        rigidbody = GetComponent<Rigidbody>(); 
    }

    private void Update()
    {
        if (ShouldJump())
        { 
            Jump(); 
        }
    }

    private bool ShouldJump()
    { 
        if (jumpInput)
        {
            if (rigidbody.linearVelocity.y == 0)
            {
                return Physics.Raycast(footPivot.position, Vector3.down, out RaycastHit hit, groundedDistance);
            }
        }
        return false;
    }

    protected void Jump()
    {
        rigidbody.AddForce(Vector3.up * force, ForceMode.Impulse); 
    }

    public void SetJumpInput(bool value)
    { jumpInput = value; }
}
