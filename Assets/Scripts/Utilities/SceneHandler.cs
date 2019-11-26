using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneHandler : MonoBehaviour
{
    public string thisSceneName;

	bool gameIsPaused;

    private void Start()
    {
        Time.timeScale = 1;

        if (thisSceneName == "AlleyScene")
        {
            LoadASceneAsync("Title");
        }
    }

    public void LoadASceneAsync(string sceneName)
    {
        SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);

        if (thisSceneName != "AlleyScene")
        {
            SceneManager.UnloadSceneAsync(thisSceneName);
        }
    }

    public void LoadNextLevel(string sceneName)
	{
        LoadASceneAsync(sceneName);
        Debug.LogError("Load Next Level is deprecated.");
	}

    public void QuitGame()
    {
        Application.Quit();
    }
}
