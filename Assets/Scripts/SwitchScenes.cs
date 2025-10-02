using UnityEngine;
using UnityEngine.SceneManagement;

public class SwitchScenes : MonoBehaviour
{
    public void LoadScene1()
    {
        SceneManager.LoadScene("Scene 1");
    }

    public void LoadScene2()
    {
        SceneManager.LoadScene("Scene 2");
    }

    public void QuitGame()
    {
        PlayerPrefs.SetInt("SavedScene", SceneManager.GetActiveScene().buildIndex);
        SceneManager.LoadScene("TitleScreen");
    }
}
