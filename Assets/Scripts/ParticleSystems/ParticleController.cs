using UnityEngine;

public class ParticleController : MonoBehaviour
{
    [SerializeField] private ParticleSystem myParticleSystem;
    private ParticleSystem.CollisionModule collisionModule;

    void Start()
    {
        if (myParticleSystem == null)
        {
            myParticleSystem = GetComponent<ParticleSystem>();
        }
        collisionModule = myParticleSystem.collision;
    }

    public void PlayParticles() => myParticleSystem.Play();
    public void StopParticles() => myParticleSystem.Stop();

    public bool IsAlive() => myParticleSystem.IsAlive();

    public void SetColliderLayer(string layerName)
    {
        collisionModule.collidesWith = LayerMask.GetMask(layerName);
    }
}
