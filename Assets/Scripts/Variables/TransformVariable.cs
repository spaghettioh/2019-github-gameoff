using UnityEngine;

[CreateAssetMenu]
public class TransformVariable : ScriptableObject
{
    public Transform value;

    public void SetValue(Transform t)
    {
        value = t;
    }
}
