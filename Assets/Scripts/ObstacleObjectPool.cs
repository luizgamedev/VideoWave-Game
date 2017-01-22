using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleObjectPool : ObjectPool {

	public List<GameObject> m_baseObjects = new List<GameObject>();

	public int m_numberOfBaseObjects = 30;

	Coroutine m_obstacleCoroutine = null;

	float m_minObstacleReleaseTime = 1f;
	float m_maxObstacleReleaseTime = 4f;

	float m_maxY;
	float m_minY;

	public override void Start()
	{
		for(int i = 0 ; i < m_numberOfBaseObjects ; i++)
		{
			int randomIndex = Random.Range(0, m_baseObjects.Count);
			m_poolOfObjects.Add(Instantiate( m_baseObjects[randomIndex] ));
			m_poolOfObjects[i].SetActive(false);
			m_poolOfObjects[i].GetComponent<ObstacleBehaviour>().SetObjectPool(this);
		}

		base.Start();

		m_minY = CameraBehaviour.Instance.m_cameraBottomReference.transform.position.y;
		m_maxY = CameraBehaviour.Instance.m_cameraTopReference.transform.position.y;

		m_obstacleCoroutine = StartCoroutine(SpawnObstacle());
	}

	IEnumerator SpawnObstacle()
	{
		yield return new WaitForSecondsRealtime(5f);

		while(true)
		{
			ReleaseObject();
			yield return new WaitForSecondsRealtime(Random.Range(m_minObstacleReleaseTime, m_maxObstacleReleaseTime));
		}
	}

	public override void ReleaseObject()
	{
		m_poolOfObjects[m_poolIndex].transform.position = new Vector3(CameraBehaviour.Instance.m_cameraRightReference.transform.position.x + 50f, 
																		   Random.Range(m_minY + 5f, m_maxY - 5f), 
																		   m_initialLocalZ);

       base.ReleaseObject();	
	}



}
