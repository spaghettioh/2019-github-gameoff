using UnityEngine;

public class Respawn : MonoBehaviour
{
    public Transform spawner;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "Raccoon")
            gameObject.transform.position = spawner.position;
    }
}
