using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChoosePrize : MonoBehaviour
{

    [SerializeField] PuzzleAreaChecker puzzleScript;
    [SerializeField] PuzzleManager managerScript;

    [Header("Dialogue")]
    [SerializeField] Dialogue dialogueScript;

    [SerializeField] private int dialogueOne;
    [SerializeField] private int dialogueTwo;
    [SerializeField] private int dialogueThree;

    [Header("Colliders")]
    [SerializeField] BoxCollider2D colliderOne;
    [SerializeField] BoxCollider2D colliderTwo;
    [SerializeField] BoxCollider2D colliderThree;

    public bool choosingItem;
    public bool allItemsInspected;

    [SerializeField] PrizeWeb prizeOne;
    [SerializeField] PrizeWeb prizeTwo;
    [SerializeField] PrizeWeb prizeThree;

    [SerializeField] Dialogue dialogue;

    void Update()
    {

        if (!allItemsInspected && prizeOne.alreadyInspected && prizeTwo.alreadyInspected && prizeThree.alreadyInspected && !dialogue.waiting)
        {
            dialogue.EndDialogue();

            StartCoroutine(Wait());
        }
        
        if (allItemsInspected && !dialogue.waiting)
            choosingItem = true;

        if (Input.GetMouseButtonDown(0) && allItemsInspected && !dialogueScript.waiting && choosingItem)
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);

            if (hit.collider != null && hit.collider == colliderOne)
            {
                Debug.Log("You chose the gold.");
                dialogueScript.indexStart = dialogueOne;
                dialogueScript.indexEnd = dialogueOne;
                StartCoroutine(CallClosePuzzle());
            }
            else if (hit.collider != null && hit.collider == colliderTwo)
            {
                Debug.Log("You chose the flower.");
                dialogueScript.indexStart = dialogueTwo;
                dialogueScript.indexEnd = dialogueTwo;
                StartCoroutine(CallClosePuzzle());
            }
            else if (hit.collider != null && hit.collider == colliderThree)
            {
                Debug.Log("You chose the hand.");
                dialogueScript.indexStart = dialogueThree;
                dialogueScript.indexEnd = dialogueThree;
                StartCoroutine(CallClosePuzzle());
            }
        }
    }

    void SelectingItem()
    {
        dialogueScript.indexStart = 9;
        dialogueScript.indexEnd = 9;
        dialogueScript.StartDialogue();


        allItemsInspected = true;
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(0.5f);

        SelectingItem();
    }

    IEnumerator CallClosePuzzle()
    {
        choosingItem = false;
        dialogueScript.StartDialogue();

        while (dialogue.waiting)
            yield return null;

        //yield return new WaitForSeconds(1);
        colliderOne.enabled = false;
        colliderTwo.enabled = false;
        colliderThree.enabled = false;
        //managerScript.ClosePuzzle();
    }

}
