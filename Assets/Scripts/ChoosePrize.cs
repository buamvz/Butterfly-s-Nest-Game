using UnityEngine;
using UnityEngine.SceneManagement;

public class ChoosePrize : MonoBehaviour
{

    [SerializeField] PuzzleAreaChecker puzzleScript;

    [Header("Dialogue for choosing Prize")]
    [SerializeField] Dialogue dialogueScript;
    [SerializeField] private int dialogueStartLine;
    [SerializeField] private int dialogueEndLine;

    [Header("Colliders")]
    [SerializeField] PolygonCollider2D goldCollider;
    [SerializeField] PolygonCollider2D flowerCollider;
    [SerializeField] PolygonCollider2D handCollider;

    public bool choosingItem;


    // Update is called once per frame
    void Update()
    {


        if (Input.GetMouseButtonDown(0) && puzzleScript.allItemsInspected && !dialogueScript.waiting)
        {
            choosingItem = true;

            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);

            if (hit.collider != null && hit.collider == goldCollider)
            {
                Debug.Log("You chose the gold.");
                ClosePuzzle();
            }
            else if (hit.collider != null && hit.collider == flowerCollider)
            {
                Debug.Log("You chose the flower.");
                ClosePuzzle();
            }
            else if (hit.collider != null && hit.collider == handCollider)
            {
                Debug.Log("You chose the hand.");
                ClosePuzzle();
            }
            else
                SelectingItem();
        }
    }


    void SelectingItem()
    {
        dialogueScript.indexStart = 9;
        dialogueScript.indexEnd = 9;
        dialogueScript.StartDialogue();
    }

    void ClosePuzzle()
    {
        Time.timeScale = 1f;
        SceneManager.UnloadSceneAsync("Puzzle1_Web");
        Debug.Log("puzzle closed, Scene2 reactivated.");
    }
}
