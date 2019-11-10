using UnityEngine;

[CreateAssetMenu]
public class BooleanVariable : ScriptableObject
{
    public bool value;

    public void SetValue(bool v)
    {
        value = v;
    }
}
