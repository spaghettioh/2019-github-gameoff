using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformGetterSetter : MonoBehaviour
{
    public enum Occurrence { Awake, Start, Update, FixedUpdate };

    public Transform get;
    public TransformVariable set;

    public Occurrence whenToSet = Occurrence.Update;

	void Awake()
	{
		if (whenToSet == Occurrence.Awake)
		{
			set.value = get;
		}
	}

	void Start()
	{
		if (whenToSet == Occurrence.Start)
		{
			set.value = get;
		}
	}

	void Update()
	{
		if (whenToSet == Occurrence.Update)
		{
			set.value = get;
		}
	}

	void FixedUpdate()
    {
		if (whenToSet == Occurrence.FixedUpdate)
		{
			set.value = get;
		}
    }
}
