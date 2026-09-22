using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerJumpBehaviour : MonoBehaviour
{
    [SerializeField] protected Transform footPivot;
    [SerializeField] protected float groundedDistance;
    [SerializeField] protected LayerMask groundLayer;
    [SerializeField] protected float force;
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
            return Physics.Raycast(footPivot.position, Vector3.down, groundedDistance, groundLayer);
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
