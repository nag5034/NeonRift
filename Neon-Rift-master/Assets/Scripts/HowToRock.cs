using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HowToRock : MonoBehaviour 
{
	public Button howToBtn;

	// Use this for initialization
	void Start () 
	{
		Button startUp = howToBtn.GetComponent<Button>();
		startUp.onClick.AddListener(LoadScreen);
	}

	// Update is called once per frame
	void Update () 
	{

	}

	void LoadScreen() 
	{
		SceneManager.LoadScene("How To Rock");
	}
}
