using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cullOffscreen : MonoBehaviour
{

	private Spawning spawningScript;

	// Use this for initialization
	void Start ()
	{
		spawningScript = GameObject.Find("Game Manager").GetComponent<Spawning>();
	}
	
	// Update is called once per frame
	void Update ()
	{
		Vector3 myPosition = Camera.main.WorldToViewportPoint(transform.position);

		// If an object containing this script goes off left-screen, destroy it immediately
		if (myPosition.x < 0)
		{
			if(gameObject.name.Substring(0, 5) == "Enemy")
			{
				spawningScript.cullEnemy();
			}

			Destroy(gameObject);
		}
	}
}
