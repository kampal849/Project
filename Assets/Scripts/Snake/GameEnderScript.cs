using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEnderScript : MonoBehaviour 
{
	AudioSource audioSource;

	void Start()
	{
		audioSource = GetComponent<AudioSource>();
		StartCoroutine(Wait());
	}

	IEnumerator Wait() 
	{
		while(audioSource.isPlaying)
			yield return null;
		yield return new WaitForSeconds(1);

		UnityEngine.SceneManagement.SceneManager.LoadScene("Snake_Menu");
	}
}
