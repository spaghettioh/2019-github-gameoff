using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Checks for any input whatsoever and triggers unity events based on configured hold timings.
/// </summary>
public class HandleInput : MonoBehaviour
{
    [Tooltip("If released after this time and before short hold wait, ignore the button press altogether.")]
    public float ignorePressAfter = 0.5f;
    public bool activateImmediately;
    public UnityEvent onKeyPressed;

    [Space]
    [Space]
    public float shortHoldWait = 1;
    public bool activateAtShort;
    public UnityEvent onKeyShortHold;

    [Space]
    [Space]
    public float longHoldWait = 1.5f;
    public bool activateAtLong;
    public UnityEvent onKeyLongHold;

    [Space]
    [Space]
    [Tooltip("This allows other scripts to know how long inputs are held.")]
    public FloatVariable holdTimer;

    float keyDownTimer;
    protected List<KeyCode> activeInputs = new List<KeyCode>();
        List<KeyCode> pressedInput;
        List<KeyCode> releasedInputs;
    bool keyHeldPrevious;

    // Check the configuraiton.
    private void Start()
    {
        if (shortHoldWait < ignorePressAfter)
            Debug.LogError("Short Hold Wait (" + shortHoldWait + ") can't be lower than Ignore Press After (" + ignorePressAfter + ")");

        if (longHoldWait < shortHoldWait)
            Debug.LogError("Long Hold Wait (" + longHoldWait + ") can't be lower than Short Hold Wait (" + shortHoldWait + ")");

        if (Input.anyKey || Input.anyKeyDown)
        {
            keyHeldPrevious = true;
            Debug.Log("keyHeldPrevious");
            foreach (KeyCode code in System.Enum.GetValues(typeof(KeyCode)))
            {
                if (Input.GetKey(code))
                {
                    Debug.Log(code);
                }
            }
        }

        holdTimer.value = 0;
    }

    public void Update()
    {

        if (holdTimer)
            holdTimer.value = keyDownTimer;

        BuildActiveInputs();
        BuildReleasedInputs();

        // These are for immediate activation instead of release activation
        if (!keyHeldPrevious)
        {
            if (activeInputs.Count > 0 && activateImmediately)
            {
                onKeyPressed.Invoke();
                keyHeldPrevious = true;
                holdTimer.value = 0;
                return;
            }
            if (keyDownTimer > shortHoldWait && activateAtShort)
            {
                onKeyShortHold.Invoke();
                keyHeldPrevious = true;
                holdTimer.value = 0;
                return;
            }
            if (keyDownTimer > longHoldWait && activateAtLong)
            {
                onKeyLongHold.Invoke();
                keyHeldPrevious = true;
                holdTimer.value = 0;
                return;
            }
        }

        // This condition means all keys were released just now
        if (releasedInputs.Count == 0 && activeInputs.Count != 0)
        {
            if (keyDownTimer > longHoldWait)
            {
                onKeyLongHold.Invoke();
            }
            else if (keyDownTimer > shortHoldWait && keyDownTimer < longHoldWait)
            {
                onKeyShortHold.Invoke();
            }
            else if (keyDownTimer > 0 && keyDownTimer < ignorePressAfter)
            {
                onKeyPressed.Invoke();
            }

            keyDownTimer = 0;
            keyHeldPrevious = false;
        }

        activeInputs = releasedInputs;
    }

    void BuildActiveInputs()
    {
        // Record which inputs are pressed
        pressedInput = new List<KeyCode>();

        if (Input.anyKeyDown || Input.anyKey)
        {
            foreach (KeyCode code in System.Enum.GetValues(typeof(KeyCode)))
            {
                if (Input.GetKey(code))
                {
                    activeInputs.Remove(code);
                    activeInputs.Add(code);
                    pressedInput.Add(code);
                }
            }

            // Don't count input holds when still held from the last thing that activated this one
            if (!keyHeldPrevious)
            {
                keyDownTimer += Time.deltaTime;
            }
        }
    }

    void BuildReleasedInputs()
    {
        // Record which held inputs were just released
        releasedInputs = new List<KeyCode>();

        foreach (KeyCode code in activeInputs)
        {
            releasedInputs.Add(code);

            if (!pressedInput.Contains(code))
            {
                releasedInputs.Remove(code);
            }
        }
    }
}
