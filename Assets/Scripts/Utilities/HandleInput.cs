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
    public UnityEvent onKeyPressed;

    [Space]
    public float shortHoldWait = 1;
    public UnityEvent onKeyShortHold;

    [Space]
    public float longHoldWait = 1.5f;
    public UnityEvent onKeyLongHold;

    float keyDownTimer;
    protected List<KeyCode> activeInputs = new List<KeyCode>();

    // Check the configuraiton.
    private void Start()
    {
        if (shortHoldWait < ignorePressAfter)
        {
            Debug.LogError("Short Hold Wait (" + shortHoldWait +
                ") can't be lower than Ignore Press After (" + ignorePressAfter + ")");
        }

        if (longHoldWait < shortHoldWait)
        {
            Debug.LogError("Long Hold Wait (" + longHoldWait +
                ") can't be lower than Short Hold Wait (" + shortHoldWait + ")");
        }
    }

    public void Update()
    {
        // Record which inputs are pressed
        List<KeyCode> pressedInput = new List<KeyCode>();

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
            keyDownTimer += Time.deltaTime;
        }

        // Record which held inputs were just released
        List<KeyCode> releasedInput = new List<KeyCode>();

        foreach (KeyCode code in activeInputs)
        {
            releasedInput.Add(code);

            if (!pressedInput.Contains(code))
            {
                releasedInput.Remove(code);
            }
        }

        // This condition means all keys were released just now
        if (releasedInput.Count == 0 && activeInputs.Count != 0)
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
        }

        activeInputs = releasedInput;
    }
}
