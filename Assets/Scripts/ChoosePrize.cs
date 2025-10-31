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
    [SerializeField] private bool dontClose;

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
    public void HandlePrizeSelection()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);

        if (hit.collider == null) return;

        for (int i = 0; i < prizes.Count; i++)
        {

            if (dialogueScript == null)
            {
                Debug.LogError("DialogueScript is not assigned!");
                return;
            }

            if (i >= dialogueIndexes.Count)
            {
                Debug.LogError("dialogueIndexes missing entry for prize index " + i);
                return;
            }


            var prize = prizes[i];
            if (hit.collider == prize.GetCollider() && Input.GetMouseButtonDown(0))
            {
                Debug.Log($"You chose the {prize.prizeName}.");

                if (EndingDecider.Instance != null)
                {
                    EndingDecider.Instance.AddPoints(prize.GetScore());
                }
                else
                {
                    Debug.LogWarning("[ChoosePrize] EndingDecider not found in scene!");
                }

                dialogueScript.indexStart = dialogueIndexes[i];
                dialogueScript.indexEnd = dialogueIndexes[i];
                dialogueScript.StartDialogue();

                if (!dontClose)
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

