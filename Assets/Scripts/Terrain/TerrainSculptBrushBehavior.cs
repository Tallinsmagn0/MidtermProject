using UnityEngine;
using UnityEngine.Events;

public class TerrainSculptBrushBehavior : MonoBehaviour
{
    // Brush variables and references
    [SerializeField] private int maxBrushSize;
    [SerializeField] private int minBrushSize;
    [SerializeField] private UnityEvent<int> OnBrushSizeChange;
    private int brushSize = 5;

    public int GetBrushSize()
    {
        return brushSize;
    }

    public void SetBrushSize(int newSize)
    {
        brushSize = Mathf.Clamp(newSize, minBrushSize, maxBrushSize);
        OnBrushSizeChange?.Invoke(brushSize);
    }
}
