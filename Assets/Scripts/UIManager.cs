using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

	public Image paused;
	public Image pausedIcon;
	public Image pausedIcon2;
	public Image pauseButton;
	public Image playButton;
	public bool isPaused = false;

	void Start()
	{
		paused.enabled = false;
		pausedIcon.enabled = false;
		playButton.enabled = false;
	}

	void Update()
	{
		if (isPaused) {
			playButton.enabled = true;
			pauseButton.enabled = false;
			paused.enabled = true;
			pausedIcon.enabled = true;
			pausedIcon2.enabled = true;

		} else {
			pauseButton.enabled = true;
			playButton.enabled = false;
			paused.enabled = false;
			pausedIcon.enabled = false;
			pausedIcon2.enabled = false;
		}
	}

}
