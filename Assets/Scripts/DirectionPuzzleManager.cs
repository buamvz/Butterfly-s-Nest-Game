using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectionPuzzleManager : MonoBehaviour
{
    //need area dialogue and link to other scenes

    public Dialogue dialogueScript;
    [SerializeField] ConfirmPath confirm;
    [SerializeField] SceneLoader loadScene;

    [SerializeField] public int dialogueStartLine;
    [SerializeField] public int dialogueEndLine;

    [SerializeField] Animator anim;
    [SerializeField] List<BoxCollider2D> directionColliders;

    private bool correct;

    [Header("Puzzle Info")]
    public bool puzzlePlaying;

    //have direction order - middle, left, right, right 
    [SerializeField] List<string> correctSequence = new List<string>();
    [SerializeField] List<string> playerSequence = new List<string>();

    public int sequenceIndex = 0;

    private string thisDirection;
    private bool confirmingDirection;

    [SerializeField] List<SpriteRenderer> swirlSprites;

    //last direction

    private void Awake()
    {
        foreach (SpriteRenderer sprite in swirlSprites)
        {
            sprite.enabled = false;
        }
    }

    void Update()
    {
        if (dialogueScript.waiting == true)
            return;



        if (Input.GetMouseButtonDown(0) && !dialogueScript.waiting && !confirmingDirection && puzzlePlaying)
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);

            if (hit.collider != null && hit.collider.CompareTag("GoForward"))
            {
                thisDirection = "Middle";
                dialogueScript.indexStart = 0;
                dialogueScript.indexEnd = 0;

                StartCoroutine(ConfirmDirection());
            }
            else if (hit.collider != null && hit.collider.CompareTag("GoLeft"))
            {
                thisDirection = "Left";
                dialogueScript.indexStart = 1;
                dialogueScript.indexEnd = 1;

                StartCoroutine(ConfirmDirection());
            }
            else if (hit.collider != null && hit.collider.CompareTag("GoRight"))
            {
                thisDirection = "Right";
                dialogueScript.indexStart = 2;
                dialogueScript.indexEnd = 2;

                StartCoroutine(ConfirmDirection());
            }
        }
        else if (!puzzlePlaying && (Input.GetMouseButtonDown(0)))
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);

            if (hit.collider != null && hit.collider.CompareTag("GoForward"))
            {
                Debug.Log("leave");

                anim.SetTrigger("GoMid");
                loadScene.LoadNextScene();
            }
        }
    }

    public void ToggleActive()
    {
        foreach (BoxCollider2D collider in directionColliders)
        {
            collider.enabled = !collider.enabled;
        }
    }

    void ChooseDirection()
    {
        if (thisDirection == "Middle")
        {
            anim.SetTrigger("GoMid");
        }else if (thisDirection == "Left")
        {
            anim.SetTrigger("GoLeft");
        }else if (thisDirection == "Right")
        {
            anim.SetTrigger("GoRight");
        }


        CheckDirection();
        //work out actual puzzle
    }

    public void CheckDirection()
    {
        if(playerSequence[sequenceIndex] == correctSequence[sequenceIndex])
        {
            StartCoroutine(Correct());

            correct = true;

        }
        else
        {
            correct = false;
            StartCoroutine(Wrong());
        }


    }

    IEnumerator ConfirmDirection()
    {
        confirmingDirection = true;

        ToggleActive();
        dialogueScript.StartDialogue();
        
        confirm.ShowOptions(true);

        confirm.confirming = true;

        //waiting for input
        while (confirm.confirming)
        {
            yield return null;
        }

        confirmingDirection = false;
        ToggleActive();
        dialogueScript.EndDialogue();


        //yes or no
        if (confirm.confirmPath)
        {
            playerSequence.Add(thisDirection);
            ChooseDirection();
        }
        else if (!confirm.confirmPath)
        {
            dialogueScript.indexStart = 7;
            dialogueScript.indexEnd = 7;

            dialogueScript.StartDialogue();
        }


    }

    IEnumerator Correct()
    {
        yield return new WaitForSeconds(1);
        swirlSprites[sequenceIndex].enabled = true;
        sequenceIndex++;

        Debug.Log("correct");

        if (sequenceIndex == correctSequence.Count)
        {
            Debug.Log("All correct!");
            sequenceIndex = 0;
            puzzlePlaying = false;

            directionColliders[1].enabled = false;
            directionColliders[2].enabled = false;

            dialogueScript.indexStart = 4;
            dialogueScript.indexEnd = 4;
            dialogueScript.StartDialogue();
        }
    }

    IEnumerator Wrong()
    {
        yield return new WaitForSeconds(0.8f); 

        foreach (SpriteRenderer sprite in swirlSprites)
        {
            sprite.enabled = false;
        }
        Debug.Log("wrong - restart");
        playerSequence.Clear();
        sequenceIndex = 0;

        dialogueScript.indexStart = 5;
        dialogueScript.indexEnd = 5;

        dialogueScript.StartDialogue();
    }

}
