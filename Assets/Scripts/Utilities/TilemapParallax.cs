using UnityEngine;

/// <summary>
/// Move the attached object a some % of the movement of the matched object.
/// </summary>
public class TilemapParallax : MonoBehaviour
{
    public Transform relative;
    public enum ParallaxLayer { Closest, Closer, Close, None, Far, Farther, Farthest, Static};
    public ParallaxLayer parallax = ParallaxLayer.None;
    Vector3 startPosition;

    private void Awake()
    {
        startPosition = relative.position;
    }

    void FixedUpdate()
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

        transform.position = (startPosition - relative.position) * multiplier;
    }
}
