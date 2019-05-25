using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeMusicController : MonoBehaviour 
{
	public AudioClip mainMusic, endMusic;
	AudioSource audioSource;

	void Start()
	{
		audioSource = GetComponent<AudioSource> ();
	}

	public void PlayMainMusic()
	{
		audioSource.Stop ();
		audioSource.loop = true;
		audioSource.clip = mainMusic;
		audioSource.Play ();
	}

	public void StopMainMusic()
	{
		audioSource.Stop();
	}

	public void PlayEndMusic()
	{
		audioSource.Stop ();
		if (endMusic != null) {
			audioSource.loop = false;
			audioSource.clip = endMusic;
			audioSource.Play ();
		}
	}
}
