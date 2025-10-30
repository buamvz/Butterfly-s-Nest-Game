using UnityEngine;

public class PauseButton : MonoBehaviour
{
    [Header("Sprites")]
    [SerializeField] private Sprite defaultSprite;
    [SerializeField] private Sprite hoverSprite;
    [SerializeField] private Sprite clickSprite;

    [SerializeField] private PauseMenu pauseMenu; 

    private SpriteRenderer spriteRenderer;
    private BoxCollider2D col;
    private bool isHovering = false;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        col = GetComponent<BoxCollider2D>();

        if (pauseMenu == null)
            pauseMenu = FindObjectOfType<PauseMenu>();

        DontDestroyOnLoad(gameObject);
    }

    private void OnEnable()
    {
        PauseMenu.OnPauseStateChanged += HandlePauseStateChange;
    }

    private void OnDisable()
    {
        PauseMenu.OnPauseStateChanged -= HandlePauseStateChange;
    }

    private void OnMouseEnter()
    {
        if (!PauseMenu.GameIsPaused)
        {
            isHovering = true;
            spriteRenderer.sprite = hoverSprite;
        }
    }

    private void OnMouseExit()
    {
        if (!PauseMenu.GameIsPaused)
        {
            isHovering = false;
            spriteRenderer.sprite = defaultSprite;
        }
    }

    private void OnMouseDown()
    {
        if (!PauseMenu.GameIsPaused)
            spriteRenderer.sprite = clickSprite;
    }

    private void OnMouseUp()
    {
        if (pauseMenu == null) return;

        if (PauseMenu.GameIsPaused)
            pauseMenu.Resume();

        else
            pauseMenu.Pause();

        spriteRenderer.sprite = defaultSprite;
        isHovering = false;
    }

    private void HandlePauseStateChange(bool isPaused)
    {

        spriteRenderer.sprite = defaultSprite;
        spriteRenderer.enabled = !isPaused;
        if (col != null) col.enabled = !isPaused;

        isHovering = false;
    }
}
