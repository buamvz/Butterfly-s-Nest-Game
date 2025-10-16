using UnityEngine;

public class InteractionBlocker : MonoBehaviour
{
    [SerializeField] BoxCollider2D blocker;

    void OnMouseDown()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);


        if (hit.collider == blocker)
        {
            return;
            Debug.Log("Hit blocker");

        }
    }
}
