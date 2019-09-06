using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviorB : MonoBehaviour
{
    private Vector3 pos;
    public float speed;
    public float frequency;
    public float magnitude;
    private Vector3 axis;

	// The time this instance was spawned. Used for the sine wave effect, so that all of them aren't in sync
	private float timeSpawned;


    // Use this for initialization
    void Start ()
    {
        pos = transform.position;
        axis = transform.up;

		timeSpawned = Time.time;

        //rigidbody = GetComponent<Rigidbody2D>();
        //rigidbody.AddForce(new Vector2(-500.0f, 400.0f));
    }

// Update is called once per frame
void Update ()
    {
        pos -= transform.right * Time.deltaTime * speed;
		transform.position = pos + axis * Mathf.Sin((Time.time - timeSpawned) * frequency) * magnitude;
    }
}
