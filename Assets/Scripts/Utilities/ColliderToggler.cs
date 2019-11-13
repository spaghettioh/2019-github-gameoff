using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderToggler : MonoBehaviour
{
    public BooleanVariable playerGrounded;
    public BoxCollider2D col;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!playerGrounded.value)
        {
            col.enabled = true;
        } else
        {
            col.enabled = false;
        }
    }
}
