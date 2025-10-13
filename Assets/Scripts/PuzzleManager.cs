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

        //pause animtationss
        Scene scene2 = SceneManager.GetSceneByName("Scene2_bigSpider");
        if (scene2.IsValid())
        {
            List<MouseParallax> parallaxList = new List<MouseParallax>();

            GameObject[] rootObjects = scene2.GetRootGameObjects();
            foreach (GameObject go in rootObjects)
            {
                // Get all MouseParallax components in children, including inactive objects
                parallaxList.AddRange(go.GetComponentsInChildren<MouseParallax>(true));
            }

            parallaxScripts = parallaxList.ToArray();

            // Disable all parallax scripts temporarily
            foreach (MouseParallax mp in parallaxScripts)
            {
                if (mp != null)
                    mp.enabled = false;
            }
        }
        //load puzzle ontop and keep scene under overlayed
        var loadOperation = SceneManager.LoadSceneAsync(puzzleSceneName, LoadSceneMode.Additive);

        loadOperation.completed += (op) =>
        {
            //loaded scene ref
            puzzleScene = SceneManager.GetSceneByName(puzzleSceneName);

            //setting active
            SceneManager.SetActiveScene(puzzleScene);


            Debug.Log("Puzzle scene loaded and active.");
        };
    }

    public void ClosePuzzle()
    {
        //resume animation
        Scene scene2 = SceneManager.GetSceneByName("Scene2_bigSpider");
        if (scene2.IsValid())
        {
            foreach (GameObject go in scene2.GetRootGameObjects())
            {
                foreach (Animator anim in go.GetComponentsInChildren<Animator>(true))
                {
                    anim.speed = 1f; // resume animation
                }
            }
        }

        Debug.Log("Unloading puzzle scene...");
        SceneManager.UnloadSceneAsync(puzzleSceneName);

    }
}