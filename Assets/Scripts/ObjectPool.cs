using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ObjectPool : MonoBehaviour {


	public float m_relativeCameraPositionX = 15f;
	public float m_relativeCameraPositionZ = 15f;
	
	public float m_initialLocalX = 10f;
	public float m_initialLocalZ = 50f;

	[SerializeField]
	protected List<GameObject> m_poolOfObjects;

	[SerializeField]
	protected GameObject m_baseObject;

	[SerializeField]
	protected float m_refreshRate = 1f;

	protected int m_poolIndex = 0;

	private float m_lastYposition = 0f;

	private int m_numberOfPointAtSameSide = 1;

	private bool m_positiveMove = true;

	private float m_maxMove = 10f;

	Camera m_mainCam;

	public virtual void Start()
	{
		foreach(GameObject obj in m_poolOfObjects)
		{
			m_mainCam = Camera.main;
			obj.transform.SetParent(m_mainCam.transform);
			obj.transform.localPosition = new Vector3(CameraBehaviour.Instance.m_cameraRightReference.transform.localPosition.x, 0f, m_relativeCameraPositionZ);
		}
	}

	

	public virtual void ReleaseObject()
	{
		m_poolOfObjects[m_poolIndex].SetActive(true);
		
		// int randomNumber = Random.Range(1,1000);

		// if(m_numberOfPointAtSameSide < 3)
		// {
		// 	m_numberOfPointAtSameSide++;
		// 	m_lastYposition =  m_lastYposition + (Random.Range(1f, m_maxMove) * (m_positiveMove ? 1 : -1) );
		// }
		// else if(randomNumber < m_numberOfPointAtSameSide)
		// {
		// 	m_numberOfPointAtSameSide++;
		// 	m_lastYposition =  m_lastYposition + (Random.Range(1f, m_maxMove) * (m_positiveMove ? 1 : -1) );
		// }
		// else
		// {
		// 	m_numberOfPointAtSameSide = 1;
		// 	m_positiveMove = !m_positiveMove;
		// 	m_lastYposition =  m_lastYposition + (Random.Range(1f, m_maxMove) * (m_positiveMove ? 1 : -1));
		// }

		// m_lastYposition = Mathf.Clamp(m_lastYposition, -50f, 50f);

		// //Debug Line
		// m_poolOfObjects[m_poolIndex].transform.localPosition = new Vector3(m_initialLocalX, m_lastYposition, m_initialLocalZ);

		m_poolOfObjects[m_poolIndex].transform.SetParent(null);

		m_poolIndex = (m_poolIndex + 1) % m_poolOfObjects.Count;
	}

	public virtual void DeactivateObject(GameObject gameObj)
	{
		gameObj.transform.SetParent(m_mainCam.transform);
		gameObj.transform.localPosition = new Vector3(CameraBehaviour.Instance.m_cameraRightReference.transform.localPosition.x, 0f, m_initialLocalZ);
		gameObj.SetActive(false);
	}


	
}
