using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BitStrap;

public class CameraBehaviour : Singleton<CameraBehaviour> {

	public float m_XStep = 1f;

	public Transform m_cameraLeftReference;
	public Transform m_cameraRightReference;

	public Transform m_cameraTopReference;

	public Transform m_cameraBottomReference;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		transform.Translate(m_XStep * Time.deltaTime, 0f, 0f);	
	}
}
