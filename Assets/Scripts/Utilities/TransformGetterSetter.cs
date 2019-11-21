using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformGetterSetter : MonoBehaviour
{
    public Transform get;
    public TransformVariable set;

    [Header("When?")]
    public bool awake;
    public bool start;
    public bool update;
    public bool fixedUpdate;

	void Awake()
	{
		if (awake)
			set.value = get;
	}

	void Start()
	{
        if (start)
            set.value = get;
    }

    void Update()
	{
        if (update)
            set.value = get;
    }

    void FixedUpdate()
    {
        if (fixedUpdate)
            set.value = get;
    }
}
