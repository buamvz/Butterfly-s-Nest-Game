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

    [SerializeField] GameObject goodEndingConversationObject;
    [SerializeField] GameObject badEndingConversationObject;



    [SerializeField] DialogueConversationsManager mainCOnversation;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (Instance != this)
        {
            // Update conversation references on the persistent instance
            if (goodEndingConversation != null)
                Instance.goodEndingConversation = goodEndingConversation;
            if (badEndingConversation != null)
                Instance.badEndingConversation = badEndingConversation;
            if (goodEndingConversation != null)
                Instance.goodEndingConversationObject = goodEndingConversationObject;
            if (badEndingConversation != null)
                Instance.badEndingConversationObject = badEndingConversationObject;
            if (mainCOnversation != null)
                Instance.mainCOnversation = mainCOnversation;

            Destroy(gameObject); // Destroy only the duplicate GameObject, not the persistent data
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
        //wait for other conversation
        while (mainCOnversation.conversationActive)
            yield return null;

        if (totalPoints >= goodEndingThreshold)
        {
            badEndingConversationObject.SetActive(false);

            goodEndingConversation.StartConversation();

            while (goodEndingConversation.conversationActive)
                yield return null;


            SceneManager.LoadScene(goodEndingSceneName);
            Debug.Log("good ending triggered!");
        }
        else
        {
            goodEndingConversationObject.SetActive(false);

            badEndingConversation.StartConversation();

            while (badEndingConversation.conversationActive)
                yield return null;

            SceneManager.LoadScene(badEndingSceneName);
            Debug.Log("bad ending triggered!");

        }
    }
}
