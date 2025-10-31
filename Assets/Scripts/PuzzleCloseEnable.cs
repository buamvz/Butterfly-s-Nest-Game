using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PuzzleCloseEnable : MonoBehaviour
{
    [SerializeField] List<GameObject> gameObjects;
    public bool subscribed;


    private IEnumerator Start()
    {
        while (GlobalEventManager.Instance == null)
            yield return null;

        GlobalEventManager.Instance.OnPuzzleClosed += ToggleObject;
        subscribed = true;
    }



    private void OnDisable()
    {
        if (subscribed && GlobalEventManager.Instance != null)
        {
            GlobalEventManager.Instance.OnPuzzleClosed -= ToggleObject;
        }
    }


    void ToggleObject()
    {
        Debug.Log("enable convo 2");
        foreach (GameObject thing in gameObjects)
        {
            thing.SetActive(!thing.activeSelf);
        }
    }
}
