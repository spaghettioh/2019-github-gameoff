using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class TitleScreen : MonoBehaviour
{
    public FloatVariable inputHoldTimer;

    public List<MenuOption> menuOptions;

    //public Slider[] choiceSliders;
    int currentChoice = 0;

    void Start()
    {
        gameObject.SetActive(false);

        foreach (MenuOption option in menuOptions)
        {
            option.optionSlider.gameObject.SetActive(false);
        }

        menuOptions[currentChoice].optionSlider.gameObject.SetActive(true);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        menuOptions[currentChoice].optionSlider.value = inputHoldTimer.value;
    }

    public void MoveSelector()
    {
        menuOptions[currentChoice].optionSlider.gameObject.SetActive(false);
        currentChoice += 1;

        if (currentChoice == menuOptions.Count)
        {
            currentChoice = 0;
        }

        menuOptions[currentChoice].optionSlider.gameObject.SetActive(true);
    }

    public void SelectMenuOption()
    {
        menuOptions[currentChoice].onOptionSelection.Invoke();
    }
}
