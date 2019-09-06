using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviorA : MonoBehaviour
{
	private Rigidbody2D myRigidbody;

	// Use this for initialization
	void Start ()
	{
		myRigidbody = GetComponent<Rigidbody2D>();
		myRigidbody.AddForce(new Vector2(-500.0f, 0.0f));
		myRigidbody.AddTorque(Random.Range(20.0f, 200.0f));

		// TODO: Add physics here to make the enemy's arc intercept the player's, regardless of y-position
		myRigidbody.AddForce(new Vector2(0.0f, 400.0f));
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}
}
