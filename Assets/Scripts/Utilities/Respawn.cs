using UnityEngine;

public class Respawn : MonoBehaviour
{
    public GameObject spawnee;
    public Transform spawner;

    private void OnTriggerEnter2D(Collider2D c)
    {
        spawnee.transform.position = spawner.position;
    }
}
