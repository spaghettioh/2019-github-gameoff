using UnityEngine;
using UnityEngine.Playables;

public class DirtyDirtyScript : MonoBehaviour
{
    void Start()
    {
        GameObject.Find("Alley timeline").GetComponent<PlayableDirector>().Play();
    }
}
