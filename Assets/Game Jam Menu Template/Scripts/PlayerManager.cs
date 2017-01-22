using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BitStrap;

public class PlayerManager : Singleton<PlayerManager> {


	[SerializeField]
	GameObject m_playerFeedbackPrefab;
	GameObject m_playerFeedback;

	public float m_XThreshhold = 500f;

	public float m_XFixedPos = -1f;

	public float m_ZFeedbackDepth;

	Camera m_mainCamera;

	public float m_lastOnScreenPositionY = 0f;

	void Start()
	{
		m_mainCamera = Camera.main;

		if(m_playerFeedbackPrefab)
		{
			m_playerFeedback = Instantiate(m_playerFeedbackPrefab);
			Vector3 pos = m_playerFeedback.transform.position;
			pos.x = m_mainCamera.transform.position.x + m_XThreshhold;
			m_playerFeedback.transform.position = pos;
			m_ZFeedbackDepth = pos.z;

			Debug.Log(m_playerFeedback != null);
		}
	}

	void LateUpdate()
	{
		bool inputWasDone = false;

#if UNITY_EDITOR
		if(Input.GetMouseButton(0))
		{
			inputWasDone = true;

			if(m_playerFeedback != null)
			{
				Vector2 touchPos = Input.mousePosition;
				Vector3 worldPos = m_mainCamera.ScreenToWorldPoint(new Vector3(touchPos.x, touchPos.y, 20f));
				worldPos.z = m_ZFeedbackDepth;

				m_lastOnScreenPositionY = m_mainCamera.ScreenToWorldPoint(new Vector3(touchPos.x, touchPos.y, 40f)).y;

				m_playerFeedback.transform.position = new Vector3( m_mainCamera.transform.position.x + m_XFixedPos, worldPos.y, worldPos.z);

			}
		}

#elif UNITY_IOS
		for (int i = 0; i < Input.touchCount; ++i) {
            if (Input.GetTouch(i).phase == TouchPhase.Began || 
				Input.GetTouch(i).phase == TouchPhase.Moved || 
				Input.GetTouch(i).phase == TouchPhase.Stationary)
			{
				inputWasDone = true;

				if(m_playerFeedback)
				{
					Vector2 touchPos = Input.GetTouch(i).position;
					Vector3 worldPos = m_mainCamera.ScreenToWorldPoint(new Vector3(touchPos.x, touchPos.y, 20f));
					worldPos.z = m_ZFeedbackDepth;

					m_lastOnScreenPositionY = m_mainCamera.ScreenToWorldPoint(new Vector3(touchPos.x, touchPos.y, 40f)).y;

					m_playerFeedback.transform.position = new Vector3( m_mainCamera.transform.position.x + m_XFixedPos, worldPos.y, worldPos.z);
				}
				break;
			}
        }
#endif
		if(!inputWasDone)
		{
			if(m_playerFeedback)
			{
				Vector3 pos = m_playerFeedback.transform.position;
				pos.x = m_mainCamera.transform.position.x + m_XThreshhold;
				m_playerFeedback.transform.position = pos;
			}
		}

	}
}
