using UnityEngine;

public class BrushSizeScrollBehavior : MonoBehaviour
{
    [SerializeField] private TerrainSculptor ts;

    private float tempBrushSize;
    private float scrollInput;
    private bool hasScrolled = false;

    // Update is called once per frame
    void Update()
    {
        if (scrollInput != 0)
        {
            tempBrushSize = ts.GetBrushSize();;
            tempBrushSize -= scrollInput;
            hasScrolled = true;
        }
        if (hasScrolled && !ts.IsAlteringTerrain())
        {
            UpdateBrushSize();
            hasScrolled = false;
        }
    }

    private void UpdateBrushSize()
    {
        int roundedBrushSize = (int) Mathf.Round(tempBrushSize);
        ts.SetBrushSize(roundedBrushSize);
    }

    public void SetScrollInput(float scrollInput)
    {
        this.scrollInput = scrollInput;
    }
}
