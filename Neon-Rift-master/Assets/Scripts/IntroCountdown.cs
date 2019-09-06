using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroCountdown : MonoBehaviour
{
	private float timer = 0.0f;
	public float maxTime;

	// Use this for initialization
	void Start ()
	{
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		timer += Time.deltaTime;
		if(timer > maxTime)
		{
			SceneManager.LoadScene("Plot");
		}
	}
}
