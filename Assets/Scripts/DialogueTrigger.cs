using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogueScript;

    [SerializeField] BoxCollider2D colliderInteraction;
    [SerializeField] int dialogueStartLine;
    
   
    void Update()
    {
        if (dialogueScript.waiting == true)
            return;

        if (Input.GetMouseButtonDown(0) && dialogueScript.waiting == false)
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);

            if (hit.collider != null && hit.collider == colliderInteraction)
            {
                dialogueScript.indexStart = dialogueStartLine;
                //dialogueScript.indexEnd = dialogueEndLine;
                dialogueScript.StartDialogue();
            }
            //else
            //    dialogueScript.EndDialogue(); 
        }


    }

}
