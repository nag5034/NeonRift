using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Credits : MonoBehaviour 
{
	public Button creditsBtn;

	// Use this for initialization
	void Start () 
	{
		Button startUp = creditsBtn.GetComponent<Button>();
		startUp.onClick.AddListener(LoadScreen);
	}

	// Update is called once per frame
	void Update () 
	{

	}

	void LoadScreen() 
	{
		SceneManager.LoadScene("Credits");
	}
}
