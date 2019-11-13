using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextUI : MonoBehaviour
{
    public Damageable characterToMonitor;
    public Text textObject;
    // Start is called before the first frame update
    void Start()
    {
        textObject.text = characterToMonitor.currentHP.ToString();
    }

    // Update is called once per frame
    public void UpdateTextUI()
    {
        textObject.text = characterToMonitor.currentHP.ToString();
    }
}
