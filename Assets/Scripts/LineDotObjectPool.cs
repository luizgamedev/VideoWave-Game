using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineDotObjectPool : ObjectPool 
{

	public int m_poolSize;

	public float m_releaseDotsTime = 0.2f;

	public override void Start()
	{

		for(int i = 0 ; i < m_poolSize ; i++)
		{
			GameObject newDot = Instantiate(m_baseObject);
			m_poolOfObjects.Add(newDot);
			newDot.GetComponent<LineDot>().SetObjectPool(this);
		}

		base.Start();

		foreach(GameObject lineObj in m_poolOfObjects)
		{
			lineObj.GetComponent<LineDot>().SetLineActive(false);
		}

		InvokeRepeating("DebugLines", 1f, m_releaseDotsTime );
	}

	void DebugLines()
	{
		ReleaseObject();
	}

	public override void ReleaseObject()
	{
		LineDot lineDot = m_poolOfObjects[m_poolIndex].GetComponent<LineDot>();

		
		m_poolOfObjects[m_poolIndex].transform.position = new Vector3(CameraBehaviour.Instance.m_cameraRightReference.transform.position.x, 
																		   PlayerManager.Instance.m_lastOnScreenPositionY, 
																		   m_initialLocalZ);

		base.ReleaseObject();

		LineRendererManager.Instance.UpdateDots(lineDot.transform.position, true);

	}

	public override void DeactivateObject(GameObject gameObj)
	{
		LineRendererManager.Instance.UpdateDots(gameObj.transform.position, false);
		base.DeactivateObject(gameObj);
	}

	

	
	
}
