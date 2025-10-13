using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class TimelinePlayer : MonoBehaviour
{

    [SerializeField] Dialogue dialogueScript;

    [SerializeField] BoxCollider2D colliderInteraction;
    [SerializeField] PlayableDirector timelineDirector;

    //[SerializeField] GameObject interactable;
    [SerializeField] List<BoxCollider2D> collidersToDisable;
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

        //interactable.SetActive(false);
        foreach (BoxCollider2D collider in collidersToDisable)
        {
            if(collider != null)
                collider.enabled = false;
        }


        yield return new WaitForSeconds(3.25f);
        Debug.Log("spider");
        animator.SetTrigger("Active");
        
    }
}

