using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;
using UnityEngine.Playables;

public class DirtyDirtyScript : MonoBehaviour
{
    void Start()
    {
        GameObject.Find("Alley timeline").GetComponent<PlayableDirector>().Play();
    }
}
