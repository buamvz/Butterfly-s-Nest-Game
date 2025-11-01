using UnityEngine;
using UnityEngine.SceneManagement;

public class BackgroundController : MonoBehaviour
{
    public static BackgroundController Instance { get; private set; }

    [SerializeField] private GameObject defaultBackground;
    [SerializeField] private GameObject lightBackground;
    [SerializeField] private GameObject darkBackground;

    public PuzzleManager puzzleManager;

    private void Update()
    {
        if (!SceneManager.GetSceneByName("Scene5_castleHallway").isLoaded)
        {
            Debug.Log("destroy background");
            DestroyBackground();
        }
    }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

    }
    public void SetBackground(bool allLightsOn)
    {
        if (lightBackground == null || darkBackground == null)
        {
            Debug.LogWarning("backgrounds not assigned");
            return;
        }

        if (defaultBackground != null) defaultBackground.SetActive(false);
        lightBackground.SetActive(false);
        darkBackground.SetActive(false);

        if (allLightsOn)
        {
            lightBackground.SetActive(true);
        }
        else
        {
            darkBackground.SetActive(true);
        }

        Debug.Log("background chanegd to " + (allLightsOn ? "Light" : "Dark"));
    }

    public void DestroyBackground()
    {
        Destroy(gameObject);
    }

}
