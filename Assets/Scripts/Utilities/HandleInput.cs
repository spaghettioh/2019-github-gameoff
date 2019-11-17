using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HandleInput : MonoBehaviour
{
    public float keyPressIgnore;
    public UnityEvent onKeyPressed;

    [Space]
    public float keyShortHoldWait;
    public UnityEvent onKeyShortHold;

    [Space]
    public float keyLongHoldWait;
    public UnityEvent onKeyLongHold;

    float keyDownTimer = 0;
    protected List<KeyCode> activeInputs = new List<KeyCode>();

    public void Update()
    {
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
            if (keyDownTimer > keyLongHoldWait)
            {
                onKeyLongHold.Invoke();
            }
            else if (keyDownTimer > keyShortHoldWait && keyDownTimer < keyLongHoldWait)
            {
                onKeyShortHold.Invoke();
            }
            else if (keyDownTimer > 0 && keyDownTimer < keyPressIgnore)
            {
                onKeyPressed.Invoke();
            }
            else
            {
                Debug.Log("Timer was " + keyDownTimer + " which is between short and long wait ("+keyShortHoldWait+" - "+keyLongHoldWait+".");
            }

            keyDownTimer = 0;
        }

        activeInputs = releasedInput;
    }
}
