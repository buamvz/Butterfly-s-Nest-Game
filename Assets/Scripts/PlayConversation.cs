using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayConversation : MonoBehaviour
{

    //    [SerializeField] private BoxCollider2D convo2Collider;

    //    private void Awake()
    //    {
    //        convo2Collider.enabled = false;

    //        // Immediately subscribe if manager exists
    //        if (GlobalEventManager.Instance != null)
    //        {
    //            GlobalEventManager.Instance.OnPuzzleClosed += EnableCollider;
    //        }
    //    }

    //    private void Start()
    //    {
    //        // If manager didn't exist in Awake, wait for it
    //        StartCoroutine(WaitForManager());
    //    }

    //    private IEnumerator WaitForManager()
    //    {
    //        while (GlobalEventManager.Instance == null)
    //            yield return null;

    //        GlobalEventManager.Instance.OnPuzzleClosed += EnableCollider;

    //    }

    //    private void OnDisable()
    //    {
    //        if (GlobalEventManager.Instance != null)
    //        {
    //            GlobalEventManager.Instance.OnPuzzleClosed -= EnableCollider;
    //        }
    //    }

    //    private void EnableCollider()
    //    {
    //        Debug.Log("enable convo 2");
    //        convo2Collider.enabled = true;
    //    }


        [SerializeField] BoxCollider2D convo2Collider;
    public bool subscribed;


    private IEnumerator Start()
    {
        convo2Collider.enabled = false;


        while (GlobalEventManager.Instance == null)
            yield return null;

        GlobalEventManager.Instance.OnPuzzleClosed += EnableCollider;
        subscribed = true;
    }



    private void OnDisable()
    {
        if (subscribed && GlobalEventManager.Instance != null)
        {
            GlobalEventManager.Instance.OnPuzzleClosed -= EnableCollider;
        }
    }


    void EnableCollider()
    {
        Debug.Log("enable convo 2");
        convo2Collider.enabled = true;
    }

}