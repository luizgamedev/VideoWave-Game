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

	float m_deathAcumulatedTime = 0f;

	public float m_deathIdleTime = 3f;

	bool m_isRunning = false;
	bool m_playerIsDead = false;

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
		}
		
		GameEventManager.GameStart += OnStart;
		GameEventManager.GamePause += OnPause;

		OnStart();
	}

	void OnStart()
	{
		m_isRunning = true;
		Time.timeScale = 1f;
	}

	void OnPause()
	{
		m_isRunning = false;
		Time.timeScale = 0f;
	}

	void LateUpdate()
	{
		if(!m_isRunning)
		{
			if(m_playerIsDead)
			{
#if UNITY_EDITOR
				if(Input.GetMouseButtonDown(0))
				{
					m_playerIsDead = false;
					GameEventManager.TriggerGameStart();
				}
#elif UNITY_IOS
				for (int i = 0; i < Input.touchCount; ++i) {
					if (Input.GetTouch(i).phase == TouchPhase.Began)
					{
						m_playerIsDead = false;
						GameEventManager.TriggerGameStart();
						break;
					}
				}
#endif
			}
			return;
		}

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

				float newPositionY = m_mainCamera.ScreenToWorldPoint(new Vector3(touchPos.x, touchPos.y, 40f)).y;

				m_playerFeedback.transform.position = new Vector3( CameraBehaviour.Instance.m_cameraLineReference.transform.position.x - 5f, 
																   worldPos.y, worldPos.z);

				if(m_lastOnScreenPositionY == newPositionY)
				{
					m_deathAcumulatedTime += Time.deltaTime;
					if(m_deathAcumulatedTime > m_deathIdleTime)
					{
						OnPlayerDie();
					}
					else
					{
						AudioManager.Instance.SetDeathPitch(m_deathAcumulatedTime);
					}
				}
				else
				{
					m_lastOnScreenPositionY = newPositionY;
					AudioManager.Instance.ClearDeathPitch();
				}

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

					float newPositionY = m_mainCamera.ScreenToWorldPoint(new Vector3(touchPos.x, touchPos.y, 40f)).y;

					m_playerFeedback.transform.position = new Vector3( CameraBehaviour.Instance.m_cameraLineReference.transform.position.x - 5f, 
																   worldPos.y, worldPos.z);

				    if(m_lastOnScreenPositionY == newPositionY)
					{
						m_deathAcumulatedTime += Time.deltaTime;
						if(m_deathAcumulatedTime > m_deathIdleTime)
						{
							OnPlayerDie();
						}
						else
						{
							AudioManager.Instance.SetDeathPitch(m_deathAcumulatedTime);
						}
					}
					else
					{
						m_lastOnScreenPositionY = newPositionY;
						AudioManager.Instance.m_audioSourceDeathPitch();
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

			m_deathAcumulatedTime += Time.deltaTime;
			if(m_deathAcumulatedTime > m_deathIdleTime)
			{
				OnPlayerDie();
			}
			else
			{
				AudioManager.Instance.ClearDeathPitch();
			}
		}

	}

	public void OnPlayerDie()
	{
		GameEventManager.TriggerGamePause();
		m_playerIsDead = true;
		m_deathAcumulatedTime = 0f;
		AudioManager.Instance.ClearOtherFX();
		AudioManager.Instance.PlayFx("DeathSound");
	}
}
