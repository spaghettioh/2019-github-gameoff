using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneHandler : MonoBehaviour
{
    public string thisSceneName;

	bool gameIsPaused;

    private void Start()
    {
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
		SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);

        if (thisSceneName != "AlleyScene")
		{
			SceneManager.UnloadSceneAsync(thisSceneName);
		}
	}

    public void PauseUnpauseGame()
	{
		if (!gameIsPaused)
		{
			gameIsPaused = true;

			SceneManager.LoadSceneAsync("Pause", LoadSceneMode.Additive);
		}
        else
		{
			SceneManager.UnloadSceneAsync("Pause");
            gameIsPaused = false;
		}
	}

    public void QuitGame()
    {
        Application.Quit();
    }
}
