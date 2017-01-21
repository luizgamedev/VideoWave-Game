using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BitStrap;

public class LineRendererManager : Singleton<LineRendererManager> 
{
	LineRenderer m_lineRenderer = null;
	Queue<Vector3> m_dotsQueue = new Queue<Vector3>();

	public float m_initialWidth = 1f;
	public float m_endWidth = 5f;

	void Start()
	{
		m_lineRenderer = GetComponent<LineRenderer>();
		m_lineRenderer.startWidth = m_initialWidth;
		m_lineRenderer.endWidth = m_initialWidth;
		
	}

	public void UpdateDots(Vector3 position, bool active)
	{
		m_lineRenderer = GetComponent<LineRenderer>();

		if(active)
		{
			m_dotsQueue.Enqueue(position);

			Debug.Log("Set Line Active for pos: " + position);
			Vector3[] positions = m_dotsQueue.ToArray();
			for(int i = 0 ; i < positions.GetLength(0) ; i++)
			{
				m_lineRenderer.SetPosition(i, positions[i]);
			}
		}
	}
	
}
