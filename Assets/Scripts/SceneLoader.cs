using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public Animator transition;
    public float transitionTime = 1f;

    public string scene;

    //for testing
    private void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Tab))
        //{
        //    LoadNextScene();
        //}
    }

    public void LoadNextScene()
    {
        StartCoroutine(LoadScene(scene));
        Debug.Log("loading..." + scene);
    }
    IEnumerator LoadScene(string level)
    {
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(level);
    }

}
