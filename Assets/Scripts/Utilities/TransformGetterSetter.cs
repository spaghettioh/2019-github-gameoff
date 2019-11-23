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
			set.Value = get;
	}

	void Start()
	{
        if (start)
            set.Value = get;
    }

    void Update()
	{
        if (update)
            set.Value = get;
    }

    void FixedUpdate()
    {
        if (fixedUpdate)
            set.Value = get;
    }
}
