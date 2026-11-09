using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerJumpBehaviour : MonoBehaviour
{
    [SerializeField] protected Transform footPivot;
    [SerializeField] protected float groundedDistance;
    [SerializeField] protected LayerMask groundLayer;
    [SerializeField] protected float force;
    [SerializeField] protected Animator playerAnimator;
    new private Rigidbody rigidbody;
    private bool jumpInput;
    private bool isGrounded;

    private void Start()
    { 
        rigidbody = GetComponent<Rigidbody>(); 
    }

    private void Update()
    {
        SetIsGrounded();

        if (ShouldJump())
        { 
            Jump();
        }
    }

    public void SetJumpInput(bool value)
    { 
        jumpInput = value; 
    }

    private void SetIsGrounded()
    {
        isGrounded = Physics.Raycast(footPivot.position, Vector3.down, groundedDistance, groundLayer);
        playerAnimator.SetBool("IsGrounded", isGrounded);
    }

    private bool ShouldJump()
    { 
        return jumpInput && isGrounded;
    }

    protected void Jump()
    {
        rigidbody.AddForce(Vector3.up * force, ForceMode.Impulse);
        playerAnimator.SetTrigger("Jump");
        AudioManager.Instance.PlayJumpSound();
    }
}
