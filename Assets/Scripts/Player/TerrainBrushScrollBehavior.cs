using UnityEngine;

public class TerrainBrushScrollBehavior : MonoBehaviour
{
    [SerializeField] private TerrainSculptor ts;
    private float scrollInput;
    private float tempBrushSize;
    private bool hasScrolled = false;

    // Update is called once per frame
    void Update()
    {
        if (scrollInput != 0)
        {
            tempBrushSize = ts.GetBrushSize();
            tempBrushSize -= scrollInput;
            hasScrolled = true;
        }
        if (hasScrolled && !ts.IsAlteringTerrain())
        {
            ts.SetBrushSize((int) Mathf.Round(tempBrushSize));
            hasScrolled = false;
        }
    }

    public void SetScrollInput(float scrollInput)
    {
        this.scrollInput = scrollInput;
    }
}
