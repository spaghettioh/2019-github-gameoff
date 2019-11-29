using System.Collections.Generic;
using UnityEngine;

public class TitleScreen : MonoBehaviour
{
    public FloatVariable inputHoldTimer;
    public List<MenuOption> menuOptions;

    int currentChoice;

    void Start()
    {
        //gameObject.SetActive(false);

        foreach (MenuOption option in menuOptions)
        {
            option.optionSlider.gameObject.SetActive(false);
        }

        menuOptions[currentChoice].optionSlider.gameObject.SetActive(true);
    }

    void FixedUpdate()
    {
        menuOptions[currentChoice].optionSlider.value = inputHoldTimer.value * 2;
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
