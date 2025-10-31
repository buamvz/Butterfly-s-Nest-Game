using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndingDecider : MonoBehaviour
{
    public static EndingDecider Instance;

    [Header("Ending Settings")]
    public int totalPoints = 0;

    public int goodEndingThreshold = 10; //gooding ending pointed neeeded
    public string goodEndingSceneName = "GoodEnding";
    public string badEndingSceneName = "BadEnding";

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnEnable()
    {
        StartCoroutine(WaitAndSubscribe());
    }

    private IEnumerator WaitAndSubscribe()
    {
        yield return new WaitUntil(() => GlobalEventManager.Instance != null);
        GlobalEventManager.Instance.OnPointsAdded += AddPoints;
    }

    private void OnDisable()
    {
        if (GlobalEventManager.Instance != null)
            GlobalEventManager.Instance.OnPointsAdded -= AddPoints;
    }

    public void AddPoints(int amount)
    {
        totalPoints += amount;
        Debug.Log($"points added: {amount}. total now: {totalPoints}");
    }

    public void DecideEnding()
    {
        if (totalPoints >= goodEndingThreshold)
        {
            SceneManager.LoadScene(goodEndingSceneName);
            Debug.Log("good ending triggered!");
        }
        else
        {
            SceneManager.LoadScene(badEndingSceneName);
            Debug.Log("bad ending triggered!");
        }
    }
}
