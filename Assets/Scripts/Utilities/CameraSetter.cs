using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraSetter : MonoBehaviour
{
    public RenderTexture gameCameraOutput;

    /// <summary>
    /// Checks to see if this camera's scene is loaded from the AlleyScene.
    /// If it is, change this camera's output to the render texture so that
    /// it appears on the TV in the alley.
    /// If not, add some stuff so that making levels works like normal.
    /// </summary>
    void Awake()
    {
        if (SceneManager.GetActiveScene().name == "AlleyScene")
        {
            gameObject.GetComponent<Camera>().targetTexture = gameCameraOutput;
        }
        else
        {
            gameObject.AddComponent<AudioListener>();
        }
    }
}
