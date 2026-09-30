using UnityEngine;

public class KeyAndHoleLoader : MonoBehaviour, ILoader
{
    [SerializeField] private GameObject keyBallPrefab;
    [SerializeField] private GameObject keyHolePrefab;
    [SerializeField] private Transform ballStart;
    [SerializeField] private Transform holeStart;

    private GameObject keyBallObject;
    private GameObject keyHoleObject;

    public void Load()
    {
        keyBallObject = Instantiate(keyBallPrefab, ballStart.position, Quaternion.identity);
        keyHoleObject = Instantiate(keyHolePrefab, holeStart.position, Quaternion.identity);
    }

    public void Unload()
    {
        if (keyBallObject != null)
        {
            keyBallObject.SetActive(false);
        }
        if (keyHoleObject != null)
        {
            keyBallObject.SetActive(false);
        }
    }

    public void ResetBallPosition()
    {
        if (keyBallObject != null)
        {
            keyBallObject.transform.position = ballStart.position;
        }
    }
}
