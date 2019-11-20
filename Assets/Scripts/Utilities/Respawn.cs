using UnityEngine;

public class Respawn : MonoBehaviour
{
    public Transform spawner;

    private void OnTriggerEnter2D(Collider2D c)
    {
        GameObject g = c.gameObject;
        if (g.name == "Raccoon")
            g.transform.position = spawner.position;
    }
}
