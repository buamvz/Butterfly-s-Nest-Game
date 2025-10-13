using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(SpriteRenderer))]
public class ItemSelector : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    private SpriteRenderer spriteRenderer;
    private Color originalColor;
    public Color outlineColor = Color.yellow;

    public int scoreValue = 0; // 0 = bad, 1 = normal, 3 = good
    private bool isSelectable = false; 

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;
    }

    public void SetSelectable(bool value)
    {
        isSelectable = value;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (isSelectable)
            spriteRenderer.color = outlineColor;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (isSelectable)
            spriteRenderer.color = originalColor;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!isSelectable) return;

        PuzzleScoreManager.Instance.AddScore(scoreValue);
        Debug.Log($"{gameObject.name} chosen, added {scoreValue} points.");

        //turn off anymomre interactions
        isSelectable = false;
        spriteRenderer.color = originalColor;

        PuzzleScoreManager.Instance.DisableAllItemSelectors();
    }
}