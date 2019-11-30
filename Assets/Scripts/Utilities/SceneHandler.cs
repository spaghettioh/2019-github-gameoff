using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneHandler : MonoBehaviour
{
    public string thisSceneName;
    public string nextSceneName;

    private void Start()
    {
        Time.timeScale = 1;
    }

    public void LoadASceneAsync()
    {
        if (thisSceneName != "AlleyScene")
            SceneManager.UnloadSceneAsync(thisSceneName);

        SceneManager.LoadSceneAsync(nextSceneName, LoadSceneMode.Additive);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
