using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelAdvancer : MonoBehaviour
{
    public IntegerVariable levelIndex;
    public string[] levels;

    void Start()
    {
        levelIndex.value = 0;
    }

    public void NextLevel()
    {
        SceneManager.UnloadSceneAsync(levels[levelIndex.value]);
        levelIndex.value += 1;
        SceneManager.LoadSceneAsync(levels[levelIndex.value], LoadSceneMode.Additive);
    }
}
