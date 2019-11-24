using UnityEngine;

[CreateAssetMenu (menuName = "Variables/Transform")]
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
