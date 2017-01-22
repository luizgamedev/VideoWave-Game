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

		GameEventManager.GameStart += OnStart;

		OnStart();

	}

	void OnStart()
	{
		m_dotsQueue.Clear();
		DrawLines();
	}

	public void UpdateDots(Vector3 position, bool active)
	{
		if(active)
		{
			m_dotsQueue.Enqueue(position);
			DrawLines();
		}
		else
		{
			m_dotsQueue.Dequeue();
		}
	}

	public void DrawLines(){
		Vector3[] positions = m_dotsQueue.ToArray();
		for(int i = 0 ; i < 50 ; i++)
		{
			if(i < m_dotsQueue.Count)
			{
				m_lineRenderer.SetPosition(i, positions[i]);
			}
			else
			{
				m_lineRenderer.SetPosition(i, Vector3.zero);
			}
		}
	}
	
}
