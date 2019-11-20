using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelAdvancer : MonoBehaviour
{
    public IntegerVariable levelIndex;
    public string[] levels;

    // Start is called before the first frame update
    void Start()
    {
        levelIndex.value = 0;
//        SceneManager.LoadSceneAsync(levels[levelIndex.currentLevel], LoadSceneMode.Additive);
    }

    // Update is called once per frame
    void Update()
    {
        //if(Input.GetKeyDown(KeyCode.P))
        //{
        //    NextLevel();
        //}
    }

    public void NextLevel()
    {
        SceneManager.UnloadSceneAsync(levels[levelIndex.value]);
        levelIndex.value += 1;
        SceneManager.LoadSceneAsync(levels[levelIndex.value], LoadSceneMode.Additive);
    }
}
