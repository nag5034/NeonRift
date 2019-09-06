using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour 
{
	public Button startBtn;

	// Use this for initialization
	void Start () 
	{
		Button startUp = startBtn.GetComponent<Button>();
		startUp.onClick.AddListener(LoadStart);
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}

	void LoadStart() 
	{
		SceneManager.LoadScene("Scene 1");
	}
}
