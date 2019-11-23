using UnityEngine;

[CreateAssetMenu]
public class TransformVariable : ScriptableObject
{
    [SerializeField]
    Transform value;

    public Transform Value
    {
        get { return value; }
        set { this.value = value;  }
    }
}
