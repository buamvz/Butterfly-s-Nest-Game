using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PrizeClick : MonoBehaviour
{
    [SerializeField] Prize prizeScript;
    [SerializeField] PuzzleAreaChecker puzzleScript;

    [Header("Dialogue for choosing Prize")]
    [SerializeField] Dialogue dialogueScript;
    [SerializeField] private int dialogueStartLine;
    [SerializeField] private int dialogueEndLine;


    public bool isClickable = false;
    public bool alreadyInspected = false;
    private Prize prize;

    private SpriteRenderer spriteRenderer;
    private static bool itemChosen = false; // ensures player can only pick one item


    public bool choosingItem;

    void Start()
    {
        prize = GetComponent<Prize>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        PuzzleAreaChecker.OnWebCleared += EnableClicking;
    }



    void OnDestroy()
    {
        PuzzleAreaChecker.OnWebCleared -= EnableClicking;
    }

    void EnableClicking()
    {
        isClickable = true;
    }

    void SelectItem()
    {
        choosingItem = true;

        dialogueScript.indexStart = 9;
        dialogueScript.indexEnd = 9;
        dialogueScript.StartDialogue();
    }

    void OnMouseDown()
    {
        if (!isClickable) //|| itemChosen || alreadyInspected
            return;

        if (isClickable && !dialogueScript.waiting)
        {
            alreadyInspected = true;
        }
        
        //struggle is here
        //if (!prizeScript.selectPrize && !dialogueScript.waiting && puzzleScript.allItemsInspected && !choosingItem)
        //{
        //    SelectItem();
        //    Debug.Log("select an item");
        //}
        //else
        //    return;


        //if (choosingItem)
        //    StartCoroutine(ChooseItem());
    }

    void ClosePuzzle()
    {
        Time.timeScale = 1f;
        SceneManager.UnloadSceneAsync("Puzzle1_Web");
        Debug.Log("puzzle closed, Scene2 reactivated.");
    }

    //IEnumerator ChooseItem()
    //{
    //    PointCalculator.Instance.AddScore(prize.score);
    //    itemChosen = true;

    //    dialogueScript.indexStart = dialogueStartLine;
    //    dialogueScript.indexEnd = dialogueEndLine;
    //    dialogueScript.StartDialogue();

    //    while (dialogueScript.waiting)
    //    {
    //        yield return new WaitForSeconds(1);
    //    }
    //    yield return new WaitForSeconds(1);
    //    //ClosePuzzle();

    //}
}