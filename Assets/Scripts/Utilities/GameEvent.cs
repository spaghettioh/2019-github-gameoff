using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class GameEvent : MonoBehaviour
{
    [Header("What will happen?")]
    public UnityEvent gameEvent;

    [Header("When?")]
    public bool awake;
    public bool start;
    public bool update;
    public bool fixedUpdate;

    [SerializeField]
    [TextArea]
    private string note = "The event can be invoked manually by calling\nManuallyFire(float seconds).";

    [Header("Delayed?")]
    public float delayTime;

    [Header("Log debug messages?")]
    public bool showDebug;


    void Awake()
    {
        if (awake)
        {
            if (showDebug)
            {
                Debug.Log(gameObject.name + " is awake.");
            }

            StartCoroutine("FireEvent");
        }
    }

    void Start()
    {
        if (start)
        {
            if (showDebug)
            {
                Debug.Log(gameObject.name + " has started.");
            }

            StartCoroutine("FireEvent");
        }
    }

    void Update()
    {
        if (update)
        {
            if (showDebug)
            {
                Debug.Log(gameObject.name + " is updating.");
            }

            StartCoroutine("FireEvent");
        }
    }

    void FixedUpdate()
    {
        if (fixedUpdate)
        {
            if (showDebug)
            {
                Debug.Log(gameObject.name + " is fixed updating.");
            }

            StartCoroutine("FireEvent");
        }
    }

    public void ManuallyFire(float seconds = 0)
    {
        if (showDebug)
        {
            Debug.Log(gameObject.name + " is being asked to fire.");
        }

        if (seconds > 0)
        {
            delayTime = seconds;
        }
        StartCoroutine("FireEvent");
    }

    IEnumerator FireEvent()
    {
        if (delayTime > 0)
        {
            yield return new WaitForSeconds(delayTime);

        }

        if (showDebug)
        {
            Debug.Log(gameObject.name + " waited for " + delayTime + " and is firing now.");
        }

        gameEvent.Invoke();
    }
}
