using UnityEngine;

public class ConverationTrigger : MonoBehaviour
{
    public DialogueConversationsManager conversationScript;

    [SerializeField] BoxCollider2D colliderInteraction;


    void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);

            if (hit.collider != null && hit.collider == colliderInteraction)
            {
                conversationScript.StartConversation();
                colliderInteraction.enabled = false;
            }
        }


    }
}
