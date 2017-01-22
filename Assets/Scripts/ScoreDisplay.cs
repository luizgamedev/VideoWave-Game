using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreDisplay : MonoBehaviour {

	[SerializeField]
	Camera m_mainCamera;
	
	[SerializeField]
	Text m_displayText;

	// Use this for initialization
	void Start () 
	{
		m_mainCamera = Camera.main;
	}

	void LateUpdate()
	{
		m_displayText.text = "" + (int)m_mainCamera.transform.position.x;
	}
	
}
