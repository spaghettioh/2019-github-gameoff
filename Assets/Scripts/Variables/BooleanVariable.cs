using UnityEngine;

[CreateAssetMenu (menuName = "Variables/Boolean")]
public class BooleanVariable : ScriptableObject
{
    public bool value;

    public void SetValue(bool v)
    {
        value = v;
    }
}
