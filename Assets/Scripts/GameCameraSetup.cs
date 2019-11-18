using UnityEngine;
using Cinemachine;
using UnityEngine.SceneManagement;

public class GameCameraSetup : MonoBehaviour
{
    public RenderTexture gameCameraOutputTexture;

    public TransformVariable cinemachineTransformForParallax;

    public TransformVariable raccoonTransformForFollow;
    public CinemachineVirtualCamera cinemachineToSetFollow;

    void Start()
    {

        // Is this camera in a scene that's loaded in the Alley scene?
        // Yes: change this camera's output to the render texture so that it appears on the TV in the alley.
        // No: add an audio listener.
        if (SceneManager.GetActiveScene().name == "AlleyScene")
        {
            gameObject.GetComponent<Camera>().targetTexture = gameCameraOutputTexture;
        }
        else
        {
            gameObject.AddComponent<AudioListener>();
        }

        // Set the cinmachine follow to the raccoon
        cinemachineToSetFollow.Follow = raccoonTransformForFollow.value;

        // Set the transform variable for parallax tilemaps to reference
        cinemachineTransformForParallax.SetValue(transform);
    }

    void Update()
    {
        // Update transform variable for parallax
        cinemachineTransformForParallax.SetValue(transform);
    }
}