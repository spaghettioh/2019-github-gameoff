using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelAdvancer : MonoBehaviour
{
    public LevelIndex levelIndex;
    public string[] levels;

    // Start is called before the first frame update
    void Start()
    {
        levelIndex.currentLevel = 0;
        SceneManager.LoadSceneAsync(levels[levelIndex.currentLevel], LoadSceneMode.Additive);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.P))
        {
            NextLevel();
        }
    }

    void NextLevel()
    {
        SceneManager.UnloadSceneAsync(levels[levelIndex.currentLevel]);
        levelIndex.currentLevel += 1;
        SceneManager.LoadSceneAsync(levels[levelIndex.currentLevel], LoadSceneMode.Additive);
    }
}
