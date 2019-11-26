using System;
using UnityEngine.UI;
using UnityEngine.Events;

[Serializable]
public struct MenuOption
{
    // Could be refactored to be an entire game object
    // and break it up to detect the things below
    // Then menu options could be made into prefabs
    public Slider optionSlider;
    public UnityEvent onOptionSelection;
}
