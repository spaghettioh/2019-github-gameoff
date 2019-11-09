using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraSetter : MonoBehaviour
{
    public RenderTexture gameCameraOutput;

    /// <summary>
    /// Checks to see if this camera's scene is loaded from the AlleyScene and
    /// changes the display output to the render texture so it shows up on
    /// the TV in the alley.
    /// </summary>
    void Awake()
    {
        if (SceneManager.GetActiveScene().name == "AlleyScene")
            gameObject.GetComponent<Camera>().targetTexture = gameCameraOutput;
    }
}
