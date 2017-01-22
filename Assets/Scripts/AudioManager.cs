using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BitStrap;
using UnityEngine.Audio;

public class AudioManager : Singleton<AudioManager> {

	[SerializeField]
	AudioMixer m_audioMixer;
	[SerializeField]
	AudioMixerGroup m_musicGroup;
	[SerializeField]
	AudioMixerGroup m_effectsGroup;
	[SerializeField]
	AudioSource m_audioSource;

	public List<AudioDescription> m_gameAudios;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
