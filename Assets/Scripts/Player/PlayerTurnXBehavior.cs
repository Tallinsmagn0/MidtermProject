using UnityEngine;

public class PlayerTurnXBehavior : MonoBehaviour
{
    [SerializeField] private float turnSpeed;
    private float rotation = 0;
    private float turnInput;

    private void Start()
    {
        rotation = transform.rotation.y * Mathf.Rad2Deg;
    }

    private void Update()
    { 
        Turn(); 
    }

    private void Turn()
    {
        transform.Rotate(Vector3.up * turnInput * turnSpeed);
    }

    public void SetTurnInput(float value)
    { 
        turnInput = value; 
    }
}
