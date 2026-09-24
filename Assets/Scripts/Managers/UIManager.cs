using UnityEngine;
using TMPro;

public class UIManager : Singleton<UIManager>
{
    [SerializeField] private TMP_Text brushSizeText;

    public void SetBrushSizeText(int brushSize)
    {
        brushSizeText.text = $"{brushSize}";
    }
    
}
