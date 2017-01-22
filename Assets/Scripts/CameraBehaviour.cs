using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BitStrap;

public class CameraBehaviour : Singleton<CameraBehaviour> {

	public float m_XStep = 1f;

	public float m_speedFactor = 1f;

	[SerializeField]
	float m_currentSpeed;

	public Transform m_cameraLeftReference;
	public Transform m_cameraRightReference;

	public Transform m_cameraTopReference;

	public Transform m_cameraBottomReference;

	public Transform m_cameraLineReference;

	public Vector3 m_initPosition = Vector3.zero;

	// Use this for initialization
	void Start () {
		m_initPosition = transform.position;
		GameEventManager.GameStart += OnStart;
		OnStart();
	}

	void OnStart()
	{
		transform.position = m_initPosition;
		m_currentSpeed = m_XStep;
	}
	
	// Update is called once per frame
	void Update () 
	{
		transform.Translate(m_currentSpeed * Time.deltaTime, 0f, 0f);
		m_currentSpeed += m_XStep * Time.deltaTime * m_speedFactor;
	}
}
