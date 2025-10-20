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

        Debug.Log($"unloading puzzle scene '{puzzleSceneName}'...");

        SceneManager.UnloadSceneAsync(puzzleSceneName);
    }
}