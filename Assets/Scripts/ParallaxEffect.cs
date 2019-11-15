using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxEffect : MonoBehaviour
{
    public float speedModifier;
    public Transform cam;
    private Vector3 offset;

    // Start is called before the first frame update
    void Start()
    {
        offset = transform.position - cam.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = cam.position + offset;
    }
}
