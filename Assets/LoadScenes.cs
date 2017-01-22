using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScenes : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void LoadHighScores()
	{
		SceneManager.LoadScene ("HighScoreScene");
	}

	public void LoadCredits()
	{
		SceneManager.LoadScene ("Credits");
	}

	public void StartGame()
	{
		SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex + 1);
	}

	public void LoadMenu()
	{
		SceneManager.LoadScene ("Main Menu");
	}
}
