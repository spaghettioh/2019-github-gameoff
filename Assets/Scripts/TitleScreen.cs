using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class TitleScreen : MonoBehaviour
{
    public FloatVariable inputHoldTimer;

    public Slider[] choiceSliders;
    int currentChoice = 0;

    void Start()
    {
        gameObject.SetActive(false);

        foreach (Slider choice in choiceSliders)
        {
            choice.gameObject.SetActive(false);
        }

        choiceSliders[currentChoice].gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
       choiceSliders[currentChoice].value = inputHoldTimer.value;
    }

    public void MoveSelector()
    {
        choiceSliders[currentChoice].gameObject.SetActive(false);
        currentChoice += 1;

        if (currentChoice == choiceSliders.Length)
        {
            currentChoice = 0;
        }

        choiceSliders[currentChoice].gameObject.SetActive(true);
    }
}
