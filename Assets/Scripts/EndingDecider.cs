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

    [Header("Ending Converations")]
    [SerializeField] DialogueConversationsManager goodEndingConversation;
    [SerializeField] DialogueConversationsManager badEndingConversation;

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

    public void AddPoints(int amount)
    {
        totalPoints += amount;
        Debug.Log($"[EndingDecider] added {amount} points. total = {totalPoints}");
    }

    public void DecideEnding()
    {
        StartCoroutine(PlayEnding());


    //moved to coroutine
        //if (totalPoints >= goodEndingThreshold)
        //{
        //    SceneManager.LoadScene(goodEndingSceneName);
        //    Debug.Log("good ending triggered!");
        //}
        //else
        //{
        //    SceneManager.LoadScene(badEndingSceneName);
        //    Debug.Log("bad ending triggered!");
        //}
    }

    IEnumerator PlayEnding()
    {
        if (totalPoints >= goodEndingThreshold)
        {
            goodEndingConversation.StartConversation();

            while (goodEndingConversation.conversationActive)
                yield return null;


            SceneManager.LoadScene(goodEndingSceneName);
            Debug.Log("good ending triggered!");
        }
        else
        {
            badEndingConversation.StartConversation();

            while (badEndingConversation.conversationActive)
                yield return null;

            SceneManager.LoadScene(badEndingSceneName);
            Debug.Log("bad ending triggered!");

        }
    }
}
