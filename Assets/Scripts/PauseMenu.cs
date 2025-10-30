using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.Linq;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject pauseMenuUI;

    [SerializeField] private PauseButton pauseButton;

    public static event Action<bool> OnPauseStateChanged;

    private MouseParallax[] parallaxScript;

    private readonly string[] puzzleScenes = 
        { "Puzzle1_Web", 
        "Puzzle3_Vines", 
        "Puzzle4_Lights" };

    private void Start()
    {
        parallaxScript = FindObjectsOfType<MouseParallax>(true);

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        bool isPuzzleScene = puzzleScenes.Contains(scene.name);

        if (pauseMenuUI == null)
        {
            pauseMenuUI = GameObject.Find("PauseMenuUI");
            if (pauseMenuUI == null)
                Debug.LogWarning("[PauseMenu] missing manu" + scene.name);
        }

        if (pauseButton == null)
        {
            pauseButton = FindObjectOfType<PauseButton>(true);
            if (pauseButton == null)
                Debug.LogWarning("[PauseMenu] missing button " + scene.name);
        }
        gameObject.SetActive(!isPuzzleScene);

        if (pauseButton != null)
            pauseButton.gameObject.SetActive(!isPuzzleScene);

        Debug.Log($"[PauseMenu] scene loaded: {scene.name} → PauseMenu {(isPuzzleScene ? "disabled" : "enabled")}");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }

            
        }
    }
    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
        ToggleParallax(true);
        OnPauseStateChanged?.Invoke(false);
    }
    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
        ToggleParallax(false);
        OnPauseStateChanged?.Invoke(true);

        Debug.Log("Pause menu active: " + pauseMenuUI.activeSelf);

    }

    public void LoadMenu()
    {
        SceneManager.LoadScene("TitleScreen");
        Debug.Log("loading menu");
        Time.timeScale = 1f;
    }

    public void QuitGame()
    {
        Debug.Log("Quitting game");
        Application.Quit();
    }

    private void ToggleParallax(bool enable)
    {
        if (parallaxScript == null) return;

        foreach (MouseParallax mp in parallaxScript)
        {
            if (mp != null)
            {
                mp.enabled = enable;
            }
        }
    }

}
