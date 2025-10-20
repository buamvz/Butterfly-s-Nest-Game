using UnityEngine;

public class Prize : MonoBehaviour
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

    [Header("SpriteChange")]
    [SerializeField] PuzzleAreaChecker puzzleAreaScript;

    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] Sprite defaultSprite;
    [SerializeField] Sprite hoverSprite;
    [SerializeField] Sprite holdSprite;

    [SerializeField] PolygonCollider2D polygonCollider;



    private bool mouseHover;
    public bool selectPrize;

    private void Start()
    {
        selectPrize = true;
    }

    private void Update()
    {

        if (selectPrize)
        {
            if (puzzleAreaScript.webCleared)
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


                            if (Input.GetMouseButton(0) && dialogueScript.waiting == false)
                                SelectPrize();

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

    [Header("SpriteChange")]
    [SerializeField] Dialogue dialogueScript;

    [SerializeField] int dialogueStartLine;
    [SerializeField] int dialogueEndLine;


    void SelectPrize()
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
        }
        else
        {
            dialogueScript.EndDialogue();
        }
    }

}