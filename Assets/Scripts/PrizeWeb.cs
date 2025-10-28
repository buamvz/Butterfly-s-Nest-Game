using System.Drawing;
using UnityEngine;

public class PrizeWeb : MonoBehaviour
{
    [Header("Prize score")]
    [Tooltip("0 = bad, 1 = normal, 2 = good")]

    public int score = 0; // 0 = bad, 1 = normal, 2 = good

    public string prizeName;
    public int GetScore()
    {
        return score;
    }
    //end of prize stuff

    [SerializeField] ChoosePrize prize;

    [Header("SpriteChange")]
    [SerializeField] PuzzleAreaChecker puzzleAreaScript;

    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] Sprite defaultSprite;
    [SerializeField] Sprite hoverSprite;
    [SerializeField] Sprite holdSprite;

    [SerializeField] BoxCollider2D polygonCollider; //not changing name for now lol

    [Header("Dialogue for choosing Prize")]
    [SerializeField] Dialogue dialogueScript;
    [SerializeField] int dialogueStartLine;
    [SerializeField] int dialogueEndLine;

    public bool isClickable = false;
    public bool alreadyInspected = false;
    public bool choosingItem;


    private bool isInteractable = false;
    private bool mouseHover;
    public bool selectPrize;

    private void Start()
    {
        selectPrize = true;
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

    void OnMouseDown()
    {
        if (!isClickable)
            return;
    }

    void InspectPrize()
    {
        spriteRenderer.sprite = holdSprite;

        selectPrize = false;

        Vector2 cursorPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(cursorPos, Vector2.zero);

        if (hit.collider != null && hit.collider == polygonCollider)
        {
            dialogueScript.indexStart = dialogueStartLine;
            dialogueScript.indexEnd = dialogueEndLine;
            dialogueScript.StartDialogue();

            alreadyInspected = true;
        }
        else
        {
            dialogueScript.EndDialogue();
        }
    }


    private void Update()
    {
        if (prize.choosingItem && !isInteractable)
        {
            isInteractable = true;

            gameObject.tag = "Interactable";
        }


        if (selectPrize && isClickable)
        {
            
            Vector2 cursorPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(cursorPos, Vector2.zero);

            //checking hover?
            if (hit.collider != null)
            {
                if (hit.collider == polygonCollider)
                {
                    mouseHover = true;
                    
                    if (mouseHover == true)
                    {
                        spriteRenderer.sprite = hoverSprite;


                        if (Input.GetMouseButton(0) && !dialogueScript.waiting)
                            InspectPrize();

                    }
                }
                else
                {
                    mouseHover = false;
                    spriteRenderer.sprite = defaultSprite;
                }

            }

            
        }

    }
}