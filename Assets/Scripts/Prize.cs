using System.Drawing;
using UnityEngine;

public class Prize : MonoBehaviour
{
    [Header("Prize score")]
    [Tooltip("0 = bad, 1 = normal, 2 = gosod")]

    public int score = 0; // 0 = bad, 1 = normal, 2 = good

    public string prizeName;
    public int GetScore()
    {
        return score;
    }
    //end of prize stuff

    [SerializeField] ChoosePrize prize;

    [Header("SpriteChange")]

    [SerializeField] private ChoosePrize choosePrizeScript;
    [SerializeField] private Dialogue dialogueScript;

    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Sprite defaultSprite;
    [SerializeField] private Sprite hoverSprite;
    [SerializeField] private Sprite holdSprite;


    [SerializeField] private BoxCollider2D prizeCollider;

    [Header("Dialogue")]
    [SerializeField] private int dialogueStartLine;
    [SerializeField] private int dialogueEndLine;

    [SerializeField] private bool isClickable = false;
    public bool alreadyInspected = false;
    private bool isInteractable = false;
    private bool mouseHover = false;
    public bool selectPrize = true;

    private void Start()
    {
        selectPrize = true;
        PuzzleAreaChecker.OnWebCleared += EnableClicking;
        VinesAreaChecker.OnVinesCleared += EnableClicking;
    }
    void OnDestroy()
    {
        PuzzleAreaChecker.OnWebCleared -= EnableClicking;
        VinesAreaChecker.OnVinesCleared -= EnableClicking;
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

    private void Update()
    {
        // enable tagg
        if (choosePrizeScript != null && choosePrizeScript.enabled && !isInteractable && choosePrizeScript.enabled)
        {
            isInteractable = true;
            gameObject.tag = "Interactable";
        }

        if (selectPrize && isClickable)
        {
            HandleHoverAndClick();
        }
    }

    private void HandleHoverAndClick()
    {
        Vector2 cursorPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(cursorPos, Vector2.zero);

        if (hit.collider == prizeCollider)
        {
            if (!mouseHover)
            {
                mouseHover = true;
                spriteRenderer.sprite = hoverSprite;
            }

            if (Input.GetMouseButtonDown(0) && !dialogueScript.waiting)
            {
                InspectPrize();
            }
        }
        else if (mouseHover)
        {
            mouseHover = false;
            spriteRenderer.sprite = defaultSprite;
        }
    }

    private void InspectPrize()
    {
        spriteRenderer.sprite = holdSprite;
        selectPrize = false;

        dialogueScript.indexStart = dialogueStartLine;
        dialogueScript.indexEnd = dialogueEndLine;
        dialogueScript.StartDialogue();

        alreadyInspected = true;
        Debug.Log($"{prizeName} inspected.");
    }

    public Collider2D GetCollider()
    {
        return prizeCollider;
    }

}