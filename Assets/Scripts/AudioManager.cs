using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BitStrap;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class AudioManager : Singleton<AudioManager> {

	[SerializeField]
	AudioMixer m_audioMixer;
	[SerializeField]
	AudioMixerGroup m_musicGroup;
	[SerializeField]
	AudioMixerGroup m_effectsGroup;
	[SerializeField]
	AudioSource m_audioSourceMusic;

	[SerializeField]
	AudioSource m_audioSourceFX;

	[SerializeField]
	AudioSource m_audioSourceDeathPitch;

	public List<AudioDescription> m_gameAudios;

	private Dictionary<string, AudioClip> m_audioClipDictionary = new Dictionary<string, AudioClip>();

	
	void OnEnable()
	{
		foreach(AudioDescription desc in m_gameAudios)
		{
			m_audioClipDictionary.Add(desc.m_description, desc.m_audioClip);
		}

	//Tell our 'OnLevelFinishedLoading' function to start listening for a scene change as soon as this script is enabled.
		SceneManager.sceneLoaded += OnLevelFinishedLoading;
	}

	void OnDisable()
	{
	//Tell our 'OnLevelFinishedLoading' function to stop listening for a scene change as soon as this script is disabled. Remember to always have an unsubscription for every delegate you subscribe to!
		SceneManager.sceneLoaded -= OnLevelFinishedLoading;
	}

	void Start()
	{
		GameEventManager.GameStart += () => PlayMusic("GameplaySong");
	}


	public void PlayMusic(string key)
	{
		m_audioSourceMusic.clip = m_audioClipDictionary[key];
		m_audioSourceMusic.Play();
	}

	public void PlayFx(string key)
	{
		m_audioSourceFX.clip = m_audioClipDictionary[key];
		m_audioSourceFX.Play();
	}
	

	void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
	{
		Debug.Log("Level Loaded");
		Debug.Log(scene.name);
		Debug.Log(mode);

		switch(scene.buildIndex)
		{
			case 0:
				PlayMusic("MenuSong");
				break;
			case 1:
				PlayMusic("GameplaySong");
				break;
			default:
				break;
		}
	}

	public void SetDeathPitch(float pitch)
	{
		if(!m_audioSourceDeathPitch.isPlaying)
		{
			m_audioSourceDeathPitch.clip = m_audioClipDictionary["DeathPitchSound"];
			m_audioSourceDeathPitch.Play();
		}

		m_audioSourceDeathPitch.pitch = 3 / pitch;
	}

	public void ClearDeathPitch()
	{
		m_audioSourceDeathPitch.Stop();
	}

	public void ClearOtherFX()
	{
		m_audioSourceFX.Stop();
		m_audioSourceDeathPitch.Stop();
	}
}
