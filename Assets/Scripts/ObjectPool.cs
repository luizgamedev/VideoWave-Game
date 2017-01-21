using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour {


	public float m_relativeCameraPositionX = 15f;
	public float m_relativeCameraPositionZ = 15f;
	
	public float m_initialLocalX = 10f;
	public float m_initialLocalZ = 80f;

	[SerializeField]
	protected List<GameObject> m_poolOfObjects;

	[SerializeField]
	protected GameObject m_baseObject;

	[SerializeField]
	protected float m_refreshRate = 1f;

	protected int m_poolIndex = 0;

	public virtual void Start()
	{
		foreach(GameObject obj in m_poolOfObjects)
		{
			Camera mainCam = Camera.main;
			obj.transform.SetParent(mainCam.transform);
			obj.transform.localPosition = new Vector3(m_relativeCameraPositionX, 0f, m_relativeCameraPositionZ);
		}
	}

	

	public virtual void ReleaseObject()
	{
		m_poolOfObjects[m_poolIndex].SetActive(true);
		
		//Debug Line
		m_poolOfObjects[m_poolIndex].transform.localPosition = new Vector3(m_initialLocalX, Random.Range(-15, 15f), m_initialLocalZ);
		m_poolOfObjects[m_poolIndex].transform.SetParent(null);

		m_poolIndex = (m_poolIndex + 1) % m_poolOfObjects.Count;
		
	}


	
}
