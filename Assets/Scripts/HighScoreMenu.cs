using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HighScoreMenu : MonoBehaviour {

	bool isEntered = false;
	public string stringToEdit = "Hello"; 
	public string playerName;
	public InputField playerInput; 
	public Text[] scoreNames; 
	public Text scoreName1;
	public Text scoreName2;
	public Text scoreName3;
	public float score;
	float highScore1;
	float highScore2;
	float highScore3;
	public int nameIndex;

	// Use this for initialization
	void Start () {
		TouchScreenKeyboard.Open ("", TouchScreenKeyboardType.Default, true, true, true);
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetKey (KeyCode.E)) {
			playerName = playerInput.text;
			if (playerName.Length > 3) {
				playerName = "";
				print ("Try again!");
			} else {
				print ("Yay! Your name is " + playerName);
				if (score > highScore1) {

				} else if (score > highScore2) {

				} else if (score > highScore3) {

				}

			}
		}
	}
}
