using UnityEngine;
using Cinemachine;
using UnityEngine.SceneManagement;

public class GameCameraSetup : MonoBehaviour
{
    [Tooltip("To setup the TV when the Alley scene is loaded.")]
    public RenderTexture gameCameraOutputTexture;

    [Space]
    [Tooltip("To set the follow")]
    public TransformVariable raccoonTransform;
    [Tooltip("For parallax layers")]
    public CinemachineVirtualCamera cinemachineToSetFollow;

    void Start()
    {
        // This needs to happen in Start so that the variables are set beforehand
        cinemachineToSetFollow.Follow = raccoonTransform.value;

        if (SceneManager.GetActiveScene().name == "AlleyScene")
        {
            gameObject.GetComponent<Camera>().targetTexture = gameCameraOutputTexture;
        }
        else
        {
            // Add an audio listener when not loaded in the Alley
            gameObject.AddComponent<AudioListener>();
        }

    }
}