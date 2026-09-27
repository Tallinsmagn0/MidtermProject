using UnityEngine;

public class KeyBallLoader : MonoBehaviour, ILoader
{
    [SerializeField] private GameObject keyBallPrefab;
    [SerializeField] private Transform startingTransform;

    private GameObject keyBallObject;

    public void Load()
    {
        keyBallObject = Instantiate(keyBallPrefab, startingTransform.position, Quaternion.identity);
    }

    public void Unload()
    {
        if (keyBallObject != null)
        {
            keyBallObject.SetActive(false);
        }
    }

    public void ResetPosition()
    {
        if (keyBallObject != null)
        {
            keyBallObject.transform.position = startingTransform.position;
        }
    }
}
