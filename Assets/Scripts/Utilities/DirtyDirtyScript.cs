using UnityEngine;
using UnityEngine.Playables;

public class DirtyDirtyScript : MonoBehaviour
{
    public void UnpauseAlleyTimeline()
    {
        GameObject.Find("Alley timeline").GetComponent<PlayableDirector>().Play();
    }
}
