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
    [Tooltip("Record")]
    public CinemachineVirtualCamera cinemachineToSetFollow;

    private void Awake()
    {
        // Set the cinmachine follow to the raccoon
        cinemachineToSetFollow.Follow = raccoonTransform.value;

    }
    void Start()
    {
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