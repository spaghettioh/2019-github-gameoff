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

    Camera gameCamera;

    private void Awake()
    {
        gameCamera = GetComponentInChildren<Camera>();
    }

    void Start()
    {
        // This needs to happen in Start so that the variables are set beforehand
        GetComponent<CinemachineVirtualCamera>().Follow = raccoonTransform.Value;

        if (SceneManager.GetActiveScene().name == "AlleyScene")
        {
            gameCamera.targetTexture = gameCameraOutputTexture;
        }
        else
        {
            // Add an audio listener when not loaded in the Alley
            gameObject.AddComponent<AudioListener>();
            gameCamera.targetTexture = null;
        }

    }
}