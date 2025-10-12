using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PuzzleManager : MonoBehaviour
{
    private string puzzleSceneName = "Puzzle1_Web";
    private Scene puzzleScene;
    private MouseParallax[] parallaxScripts;

    public void OpenPuzzle()
    {
        Debug.Log("Loading puzzle scene...");

        //find paralaxx scpirt
        Scene scene2 = SceneManager.GetSceneByName("Scene2_bigSpider");
        if (scene2.IsValid())
        {
            List<MouseParallax> parallaxList = new List<MouseParallax>();

            GameObject[] rootObjects = scene2.GetRootGameObjects();


            foreach (GameObject go in rootObjects)
            {
                //get all affected by parallax
                parallaxList.AddRange(go.GetComponentsInChildren<MouseParallax>(true));
            }

            parallaxScripts = parallaxList.ToArray();

            //turn parallax off
            foreach (MouseParallax mp in parallaxScripts)
            {
                if (mp != null)
                    mp.enabled = false;
            }
        }

        Time.timeScale = 0f;

        //load puzzle overtop of other scene
        var loadOperation = SceneManager.LoadSceneAsync(puzzleSceneName, LoadSceneMode.Additive);
        loadOperation.completed += (op) =>
        {
            puzzleScene = SceneManager.GetSceneByName(puzzleSceneName);
            SceneManager.SetActiveScene(puzzleScene);
            Debug.Log("Puzzle scene loaded and active.");
        };
    }

    public void ClosePuzzle()
    {
        Debug.Log("Unloading puzzle scene...");

        //unload puzzel scene
        SceneManager.UnloadSceneAsync(puzzleSceneName);

        Time.timeScale = 1f;

        //turn parallax back on
        if (parallaxScripts != null)
        {
            foreach (MouseParallax mp in parallaxScripts)
            {
                if (mp != null)
                    mp.enabled = true;
            }
        }
    }
}