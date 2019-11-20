using UnityEngine;

/// <summary>
/// Move the attached object a some % of the movement of the matched object.
/// </summary>
public class TilemapParallax : MonoBehaviour
{
    public TransformVariable parallaxTo;
    public enum ParallaxLayer { Closest, Closer, Close, None, Far, Farther, Farthest, Static};
    public ParallaxLayer parallax = ParallaxLayer.None;
    float damping = 5;
    Vector3 startPosition;

    private void Awake()
    {
        startPosition = parallaxTo.value.position;
    }

    void Update()
    {
        float multiplier;

        switch (parallax)
        {
            case ParallaxLayer.Closest:
                multiplier = 0.75f;
                break;

            case ParallaxLayer.Closer:
                multiplier = 0.5f;
                break;

            case ParallaxLayer.Close:
                multiplier = 0.25f;
                break;

            case ParallaxLayer.Far:
                multiplier = -0.25f;
                break;

            case ParallaxLayer.Farther:
                multiplier = -0.5f;
                break;

            case ParallaxLayer.Farthest:
                multiplier = -0.75f;
                break;

            case ParallaxLayer.Static:
                multiplier = -1;
                break;

            default:
                multiplier = 0;
                break;
        }

        Vector3 newPosition = (startPosition - parallaxTo.value.position) * multiplier;
        transform.position = Vector3.Lerp(transform.position, newPosition, damping * Time.deltaTime);
    }
}
