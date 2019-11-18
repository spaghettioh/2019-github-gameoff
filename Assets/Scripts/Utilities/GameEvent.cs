using UnityEngine;
using UnityEngine.Events;

public class GameEvent : MonoBehaviour
{
    public UnityEvent onStart;
    [Space]
    [Space]
    [Space]
    public UnityEvent onUpdate;
    [Space]
    [Space]
    [Space]
    public UnityEvent onFixedUpdate;
    [Space]
    [Space]
    [Space]
    public UnityEvent onManuallyFire;

    void Start()
    {
        onStart.Invoke();
    }

    void Update()
    {
        onUpdate.Invoke();
    }

    void FixedUpdate()
    {
        onFixedUpdate.Invoke();
    }

    public void ManuallyFire()
    {
        onManuallyFire.Invoke();
    }
}
