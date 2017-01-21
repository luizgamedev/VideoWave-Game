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

		base.ReleaseObject();

		//Debug Line
		m_poolOfObjects[m_poolIndex].transform.localPosition = new Vector3(m_initialLocalX, Random.Range(-15, 15f), m_initialLocalZ);

		LineRendererManager.Instance.UpdateDots(lineDot.transform.position, true);

	}

	public override void DeactivateObject(GameObject gameObj)
	{
		LineRendererManager.Instance.UpdateDots(gameObj.transform.position, false);
		base.DeactivateObject(gameObj);
	}

	

	
	
}
