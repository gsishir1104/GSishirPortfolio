using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HoverGlow : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Image image;
    public Color glowColor = new Color(0, 1, 0.6f, 0.7f);
    private Color originalColor;

    void Start()
    {
        originalColor = image.color;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        image.color = glowColor;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        image.color = originalColor;
    }
}
