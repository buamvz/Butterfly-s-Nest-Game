using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject pauseMenuUI;

    [SerializeField] private PauseButton pauseButton;

    public static event Action<bool> OnPauseStateChanged;

    private MouseParallax[] parallaxScript;

    private void Start()
    {
        parallaxScript = FindObjectsOfType<MouseParallax>(true);

        DontDestroyOnLoad(gameObject);
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
