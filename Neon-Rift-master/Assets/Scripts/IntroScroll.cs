using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroScroll : MonoBehaviour
{

	// Use this for initialization
	void Start ()
	{
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		transform.Translate(new Vector3(0.0f, 50.0f, 0.0f) * Time.deltaTime);

		if(Input.GetMouseButton(0) || transform.position.y > 1850.0f)
		{
			SceneManager.LoadScene("Main Menu");
		}
	}
}
