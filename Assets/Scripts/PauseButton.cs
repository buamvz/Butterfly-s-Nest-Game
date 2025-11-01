using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseButton : MonoBehaviour
{
    [Header("Sprites")]
    [SerializeField] private Sprite defaultSprite;
    [SerializeField] private Sprite hoverSprite;
    [SerializeField] private Sprite clickSprite;


    private SpriteRenderer spriteRenderer;
    private BoxCollider2D col;
    private bool isHovering = false;
    private bool pauseSceneLoaded = false;

    private const string pauseSceneName = "PauseMenu";


    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        col = GetComponent<BoxCollider2D>();
        DontDestroyOnLoad(gameObject); //THIS MIGHT BE THE ISSUE LATER WHNE BUTTTON IS IN ALL SCENES
    }
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        SceneManager.sceneUnloaded += OnSceneUnloaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
        SceneManager.sceneUnloaded -= OnSceneUnloaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // turn back on buttpn
        if (scene.name != pauseSceneName)
        {
            spriteRenderer.enabled = true;
            col.enabled = true;
            pauseSceneLoaded = false;
        }
    }

    private void OnSceneUnloaded(Scene scene)
    {
        if (scene.name == pauseSceneName)
        {
            spriteRenderer.enabled = true;
            col.enabled = true;
            pauseSceneLoaded = false;

            Debug.Log("[PauseButton] Pause menu closed → button re-enabled");
        }
    }
    private void OnMouseEnter()
    {
        if (!pauseSceneLoaded)
        {
            isHovering = true;
            spriteRenderer.sprite = hoverSprite;
        }
    }

    private void OnMouseExit()
    {
        if (!pauseSceneLoaded)
        {
            isHovering = false;
            spriteRenderer.sprite = defaultSprite;
        }
    }

    private void OnMouseDown()
    {
        if (!pauseSceneLoaded)
            spriteRenderer.sprite = clickSprite;
    }

    private void OnMouseUp()
    {
        if (!pauseSceneLoaded)
        {
            spriteRenderer.sprite = defaultSprite;
            isHovering = false;
            OpenPauseScene();
        }
    }

    private void OpenPauseScene()
    {
        SceneManager.LoadScene(pauseSceneName, LoadSceneMode.Additive);
        pauseSceneLoaded = true;

        spriteRenderer.enabled = false;
        col.enabled = false;

        Debug.Log($"[PauseButton] Loaded pause scene: {pauseSceneName}");
    }
}
