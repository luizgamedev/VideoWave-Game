using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineDotObjectPool : ObjectPool 
{

	public int m_poolSize;

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

		InvokeRepeating("DebugLines", 1f, 0.2f );
	}

	void DebugLines()
	{
		ReleaseObject();
	}

	public override void ReleaseObject()
	{
		LineDot lineDot = m_poolOfObjects[m_poolIndex].GetComponent<LineDot>();

		lineDot.SetLineActive(true);

		base.ReleaseObject();

		//Debug.Log("Set Line Active for pos: " + lineDot.transform.position);

		LineRendererManager.Instance.UpdateDots(lineDot.transform.position, true);

	}

	

	
	
}
