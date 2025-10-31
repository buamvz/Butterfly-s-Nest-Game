using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SwitchScenes : MonoBehaviour
{
    public static SwitchScenes instance;

    //for crossfade animation
    [SerializeField] Animator transition;
    private float transitionTime = 1f;

    //private void Start()
    //{
    //    if (instance == null)
    //    {
    //        DontDestroyOnLoad(gameObject);
    //        instance = this;
    //    }
    //    else
    //    {
    //        Destroy(gameObject);
    //    }
    //}
    public void LoadScene1()
    {
        Debug.Log("Loading Scene 1");

        StartCoroutine(FadeOut());
        SceneManager.LoadScene("Scene 1_forest Start");
    }

    public void LoadScene2()
    {
        Debug.Log("Loading Scene 2");
        
        StartCoroutine(FadeOut());
        SceneManager.LoadScene("Scene 2");
    }

    //crossfade player
    IEnumerator FadeOut()
    {
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(transitionTime);

    }


    public void BackButtom()
    {
        Debug.Log("Loading Title Screen");
        SceneManager.LoadScene("TitleScreen");
    }

    public void QuitGame()
    {
        Debug.Log("Quit Game");
        PlayerPrefs.SetInt("SavedScene", SceneManager.GetActiveScene().buildIndex);
        SceneManager.LoadScene("TitleScreen");
    }
}
