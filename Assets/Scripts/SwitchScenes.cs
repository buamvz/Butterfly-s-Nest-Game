using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SwitchScenes : MonoBehaviour
{
    public static SwitchScenes instance;

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
        SceneManager.LoadScene("Scene 1_forest Start");
    }

    public void LoadScene2()
    {
        Debug.Log("Loading Scene 2");
        SceneManager.LoadScene("Scene 2");
    }

    public void LoadSettings()
    {
        Debug.Log("Loading Settings");
        SceneManager.LoadScene("Settings");
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
