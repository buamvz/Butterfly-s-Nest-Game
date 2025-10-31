using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChoosePrize : MonoBehaviour
{
    [SerializeField] PuzzleManager managerScript;

    [Header("Dialogue")]
    [SerializeField] Dialogue dialogueScript;

    [SerializeField] private List<int> dialogueIndexes = new List<int>();

    [Header("Prizes")]
    [SerializeField] private List<Prize> prizes = new List<Prize>();

    public bool choosingItem;
    public bool allItemsInspected;
    [SerializeField] private bool webPrize;

    [SerializeField] Dialogue dialogue;

    private void Update()
    {
        if (!allItemsInspected && AllPrizesInspected() && !dialogueScript.waiting)
        {
            dialogueScript.EndDialogue();
            StartCoroutine(Wait());
        }

        if (allItemsInspected && !dialogueScript.waiting)
            choosingItem = true;

        if (choosingItem && !dialogueScript.waiting)
        {
            HandlePrizeSelection();
        }
    }

    private bool AllPrizesInspected()
    {
        foreach (var prize in prizes)
        {
            if (!prize.alreadyInspected)
                return false;
        }
        return true;
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(0.5f);

        SelectingItem();
    }

    void SelectingItem()
    {
        dialogueScript.indexStart = 9;
        dialogueScript.indexEnd = 9;
        dialogueScript.StartDialogue();


        allItemsInspected = true;
    }
    private void HandlePrizeSelection()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);

        if (hit.collider == null) return;

        for (int i = 0; i < prizes.Count; i++)
        {
            var prize = prizes[i];
            if (hit.collider == prize.GetCollider())
            {
                Debug.Log($"You chose the {prize.prizeName}.");

                EndingDecider.Instance.DecideEnding();

                dialogueScript.indexStart = dialogueIndexes[i];
                dialogueScript.indexEnd = dialogueIndexes[i];

                if(!webPrize)
                    StartCoroutine(CallClosePuzzle());

                break;
            }
        }
    }

    private IEnumerator CallClosePuzzle()
    {
        choosingItem = false;
        dialogueScript.StartDialogue();

        while (dialogueScript.waiting)
            yield return null;

        // disable all prize colliders 
        foreach (var prize in prizes)
        {
            var col = prize.GetCollider();
            if (col != null)
                col.enabled = false;
        }

        managerScript.ClosePuzzle();
    }
}

