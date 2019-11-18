using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneHandler : MonoBehaviour
{
    public string thisSceneName;

    private void Start()
    {
        if (SceneManager.GetActiveScene().name == "AlleyScene")
        {
            LoadASceneAsync("Title");
        }
    }
    public void LoadASceneAsync(string sceneName)
    {
        print(SceneManager.GetAllScenes());
        SceneManager.UnloadSceneAsync(thisSceneName); ;
        SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
    }
}
