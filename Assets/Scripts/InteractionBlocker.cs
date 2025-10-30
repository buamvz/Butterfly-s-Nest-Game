using UnityEngine;

public class InteractionBlocker : MonoBehaviour
{
    [SerializeField] private BoxCollider2D blocker;
    [SerializeField] private LayerMask blockingLayers;

    private void OnMouseDown()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero, Mathf.Infinity, blockingLayers);

        if (hit.collider != null)
        {
            if (hit.collider == blocker)
            {
                Debug.Log("Interaction blocked!");
                // Optionally play a sound or visual feedback here
                return;
            }
        }
    }
}