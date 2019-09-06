using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour 
{
	public Button menuBtn;

	// Use this for initialization
	void Start () 
	{
		Button startUp = menuBtn.GetComponent<Button>();
		startUp.onClick.AddListener(LoadMenu);
	}

	// Update is called once per frame
	void Update () 
	{

	}

	void LoadMenu() 
	{
		SceneManager.LoadScene("Main Menu");
	}
}
