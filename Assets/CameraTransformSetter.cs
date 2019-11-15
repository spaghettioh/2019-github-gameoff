using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraTransformSetter : MonoBehaviour
{
    public TransformVariable thisCameraTransform;
    public TransformVariable raccoonTransform;

    private void Start()
    {
        gameObject.GetComponent<CinemachineVirtualCamera>().Follow = raccoonTransform.value;
    }

    void Update()
    {
        thisCameraTransform.SetValue(transform);
    }
}
