using System.Collections;
using UnityEngine;
using UnityEngine.Playables;

public class TImelinePlayer : MonoBehaviour
{

    [SerializeField] Dialogue dialogueScript;

    [SerializeField] BoxCollider2D colliderInteraction;
    [SerializeField] PlayableDirector timelineDirector;

    [SerializeField] GameObject interactable;
    [SerializeField] GameObject spiderObject;

    [SerializeField] Animator animator;

    private void Start()
    {
        spiderObject.SetActive(false);
    }

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
                StartCoroutine(PlayTimeline());
            }   
            else
                dialogueScript.EndDialogue();
        }


    }

    IEnumerator PlayTimeline()
    {
        spiderObject.SetActive(true);

        timelineDirector.Play();
        yield return new WaitForSeconds(3.25f);
        Debug.Log("spider");
        interactable.SetActive(false);
        animator.SetTrigger("Active");
        
    }
}

