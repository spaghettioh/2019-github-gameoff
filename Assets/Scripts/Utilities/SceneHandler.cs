using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneHandler : MonoBehaviour
{
    public string thisSceneName;

    private void Start()
    {
        if (thisSceneName == "AlleyScene")
        {
            LoadASceneAsync("Title");
        }
    }
    public void LoadASceneAsync(string sceneName)
    {
        if (thisSceneName != "AlleyScene")
        {
            SceneManager.UnloadSceneAsync(thisSceneName);
        }
        SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
    }
}
