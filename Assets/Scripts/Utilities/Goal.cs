﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Cinemachine;
using UnityEngine.UI;

public class Goal : MonoBehaviour
{
    public UnityEvent onCrossed;
    public CinemachineVirtualCamera cameraToZoom;
    public Image fader;
    public Text text;
    //public HandleInput disableInput;
    //public HandleInput enableInput;
    //float fade = 0;
    Color faded = new Color(0, 0, 0, 0);
    //float show = 0;
    Color shown = new Color(1, 1, 1, 0);
    //float slow = 1;
    bool crossed;
    float slomo = 1;

    private void Start()
    {
    }

    void Update()
    {
        if (crossed)
        {
            if (slomo > 0)
            {
                slomo *= .97f;

                if (slomo < .01f)
                {
                    slomo = 0;
                }
            }

            faded.a = .6f - (.6f * slomo);
            shown.a = (1 - slomo) * 2;

            fader.color = faded;
            text.color = shown;

            cameraToZoom.m_Lens.OrthographicSize = 2 + (slomo * 2);
            Time.timeScale = slomo;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        crossed = true;
        onCrossed.Invoke();
    }
}
