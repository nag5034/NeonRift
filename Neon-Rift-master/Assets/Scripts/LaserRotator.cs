using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserRotator : MonoBehaviour
{
	private float angle;

	public float sequenceStart = 0.0f;
	public float extents = 45.0f;


	// Use this for initialization
	void Start ()
	{
		angle = Mathf.Sin(sequenceStart) * extents;
	}
	
	// Update is called once per frame
	void Update ()
	{
		sequenceStart += Time.deltaTime;

		angle = Mathf.Sin(sequenceStart) * extents;
		transform.rotation = Quaternion.Euler(0.0f, 0.0f, angle);
	}
}
