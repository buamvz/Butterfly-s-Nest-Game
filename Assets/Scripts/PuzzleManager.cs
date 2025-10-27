using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PuzzleManager : MonoBehaviour
{
    [Header("Puzzle and Scene set up)")]
    [SerializeField] private string mainSceneName; //scene

    [SerializeField] private string puzzleSceneName; //puzzle

    private Scene puzzleScene;
    private MouseParallax[] parallaxScripts;

    //forreading when puzzle is closed - sienna
    public bool puzzleCloses = false;

    public void OpenPuzzle()
    {
        if (string.IsNullOrEmpty(mainSceneName) || string.IsNullOrEmpty(puzzleSceneName))
        {
            Debug.LogError("assign puzzle and scene in inspector");
            return;
        }

        Debug.Log($"loading puzzle scene '{puzzleSceneName}'...");

        // Load puzzle additively
        var loadOperation = SceneManager.LoadSceneAsync(puzzleSceneName, LoadSceneMode.Additive);
        loadOperation.completed += (op) =>
        {
            puzzleScene = SceneManager.GetSceneByName(puzzleSceneName);
            if (puzzleScene.IsValid())
            {
                SceneManager.SetActiveScene(puzzleScene);
                Debug.Log($"Puzzle scene '{puzzleSceneName}' loaded and active.");
            }
            else
            {
                Debug.LogWarning($"PuzzleManager: Scene '{puzzleSceneName}' cant be found.");
            }
        };
    }

    public void ClosePuzzle()
    {
        if (string.IsNullOrEmpty(mainSceneName) || string.IsNullOrEmpty(puzzleSceneName))
        {
            Debug.LogError("assign puzzle and scene in inspector");
            return;
        }
        //for starting second lot of conversation
        Debug.Log("close from puzzlemanager");
        GlobalEventManager.Instance.PuzzleClosed();

        Debug.Log($"unloading puzzle scene '{puzzleSceneName}'...");


        SceneManager.UnloadSceneAsync(puzzleSceneName);


    }

    //[SerializeField] BoxCollider2D convo2Collider;

    //public void SecondSequence()
    //{
    //    convo2Collider.enabled = true;
    //}
}