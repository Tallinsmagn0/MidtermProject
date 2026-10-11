using UnityEngine;
using System.Collections;

public class SandParticleEmissionBehavior : MonoBehaviour
{
    [SerializeField] private ParticleController particleController;

    [SerializeField] private Transform playerParticlePivot;
    [SerializeField] private Transform particleSystemPivot;

    [SerializeField] private string groundLayer;
    [SerializeField] private string playerLayer;

    [SerializeField] private float emissionDuration;

    private bool isEmitting;
    private Transform emissionSource;
    private Transform emissionDestination;
    private Transform clickRef;

    void Start()
    {
        particleController.StopParticles();

        GameObject sourceObject = new GameObject("SandEmissionSourceObject");
        emissionSource = sourceObject.transform;

        GameObject destinationObject = new GameObject("SandEmissionDestinationObject");
        emissionDestination = destinationObject.transform;

        GameObject clickRefObject = new GameObject("ClickRefObject");
        clickRef = clickRefObject.transform;
    }

    void Update()
    {
        if (particleController.IsAlive())
        {
            PositionEmissionSystem();
            RotateEmissionSystem();
        }
    }

    public void EmitRaiseParticlesTo(Vector3 worldPos)
    {
        if (!isEmitting)
        {
            clickRef.position = worldPos;

            emissionDestination = clickRef;
            emissionSource = playerParticlePivot;

            particleController.SetColliderLayer(groundLayer);
            StartCoroutine(SandClickRoutine());
        }
    }

    public void EmitLowerParticlesFrom(Vector3 worldPos)
    {
        if (!isEmitting)
        {
            clickRef.position = worldPos;

            emissionDestination = playerParticlePivot;
            emissionSource = clickRef;

            particleController.SetColliderLayer(playerLayer);
            StartCoroutine(SandClickRoutine());
        }
    }

    private void PositionEmissionSystem()
    {
        particleSystemPivot.position = emissionSource.position;
    }

    private void RotateEmissionSystem()
    {
        Vector3 pointDirection = emissionDestination.position - emissionSource.position;
        particleSystemPivot.rotation = Quaternion.LookRotation(pointDirection);
    }

    private IEnumerator SandClickRoutine()
    {
        isEmitting = true;
        particleController.PlayParticles();
        yield return new WaitForSeconds(emissionDuration);
        particleController.StopParticles();
        isEmitting = false;
    }
}
