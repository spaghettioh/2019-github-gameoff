using UnityEngine;

/// <summary>
/// Move the attached object a some % of the movement of the matched object.
/// </summary>
public class TilemapParallax : MonoBehaviour
{
    public TransformVariable relativeCamera;
    public enum ParallaxLayer { Closest, Closer, Close, None, Far, Farther, Farthest, Static};
    public ParallaxLayer parallax = ParallaxLayer.None;

    float damping = 5;
    Vector3 cameraStartPosition;
    Vector3 cameraCurrentPosition;

    private void Start()
    {
        cameraStartPosition = relativeCamera.Value.position;
        cameraCurrentPosition = relativeCamera.Value.position;
    }

    void Update()
    {
        float depth;

        cameraCurrentPosition = relativeCamera.Value.position;

        switch (parallax)
        {
            case ParallaxLayer.Closest:
                depth = 0.75f;
                break;

            case ParallaxLayer.Closer:
                depth = 0.5f;
                break;

            case ParallaxLayer.Close:
                depth = 0.25f;
                break;

            case ParallaxLayer.Far:
                depth = -0.25f;
                break;

            case ParallaxLayer.Farther:
                depth = -0.5f;
                break;

            case ParallaxLayer.Farthest:
                depth = -0.75f;
                break;

            case ParallaxLayer.Static:
                depth = 1;
                // just fix the layer to the camera
                cameraStartPosition = relativeCamera.Value.position;
                break;

            default:
                depth = 0;
                break;
        }

        Vector3 newLayerPosition = (cameraStartPosition - cameraCurrentPosition) * depth;
        transform.position = Vector3.Lerp(transform.position, newLayerPosition, damping * Time.deltaTime);
    }
}
