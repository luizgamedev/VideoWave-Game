using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleBehaviour : MonoBehaviour {

	public float m_rotationSpeed;

	ObstacleObjectPool m_obstaclePool = null;

	// Use this for initialization
	void Start () {
		
	}

	public void SetObjectPool(ObstacleObjectPool _objPool)
	{
		m_obstaclePool = _objPool;
	}
	
	// Update is called once per frame
	void Update () {
		transform.Rotate((new Vector3(0f, 0f, m_rotationSpeed)) * Time.deltaTime);

		if(transform.position.x < CameraBehaviour.Instance.m_cameraLeftReference.transform.position.x)
        {
            m_obstaclePool.DeactivateObject(gameObject);
        }
	}
}
