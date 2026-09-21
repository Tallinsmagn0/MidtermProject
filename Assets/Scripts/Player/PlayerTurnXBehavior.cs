using UnityEngine;

public class PlayerTurnXBehavior : MonoBehaviour
{
    [SerializeField] private float turnSpeed;
    private float rotation = 0;
    private float turnInput;

    private void Update()
    { 
        Turn(); 
    }

    private void Turn()
    {
        rotation += turnInput * turnSpeed * Time.deltaTime;
        transform.rotation = Quaternion.AngleAxis(rotation, Vector3.up);
    }

    public void SetTurnInput(float value)
    { 
        turnInput = value; 
    }
}
